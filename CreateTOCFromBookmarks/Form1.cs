using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Redaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateTOCFromBookmarks
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        private const int SB_HORZ = 0x0;
        private const int SB_VERT = 0x1;

        public Form1()
        {
            InitializeComponent();
            // 預設為 false
            treeView1.CheckBoxes = true;
            // 預設為 true
            treeView1.ShowLines = true;
            pgdt.Columns.Add("CustID_", typeof(string));
            pgdt.Columns.Add("ORG", typeof(string));
            pgdt.Columns.Add("numS", typeof(string));
            pgdt.Columns.Add("ob", typeof(Boolean));
            pgdt.Columns.Add("NUM", typeof(int));
            pgdt.Columns.Add("NUMMO", typeof(double));
            pgdt.PrimaryKey = new DataColumn[] { pgdt.Columns["CustID_"] };
        }

        private void FocusOnRoot()
        {
            // TreeView 建置完成後，Focus 要出現在 Root 上
            this.treeView1.SelectedNode = this.treeView1.Nodes[0];
        }
        // 把點選 TreeNode 資訊顯示在 txtNodeInfo 裡
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            // 該階層索引值
            sb.AppendLine($"Index：{e.Node.Index}");
            // Name 必須在 TreeView 中是唯一的
            sb.AppendLine($"Name：{e.Node.Name}");
            // TreeNode 文字
            sb.AppendLine($"Text：{e.Node.Text}");
            // 備註說明，為 Object
            sb.AppendLine($"Tag：{e.Node.Tag}");
            // 父 TreeNode
            string Parent = e.Node.Parent == null ? "Root" : e.Node.Parent.Text;
            sb.AppendLine($"Parent：{Parent}");
            // 子節點數量
            sb.AppendLine($"Count：{e.Node.GetNodeCount(false)}");
            // 完整路徑
            sb.AppendLine($"FullPath：{e.Node.FullPath}");
            // 該 TreeNode Level 值
            sb.AppendLine($"FullPath：{e.Node.Level}");

            DataRow ppp = pgdt.Rows.Find(e.Node.Name);

            if (!(ppp is null))
            {
                sb.AppendLine($"PG-CustID_：{ppp["CustID_"]}");
                sb.AppendLine($"PG-ORG：{ppp["ORG"]}");
                sb.AppendLine($"PG-numS：{ppp["numS"]}");
                sb.AppendLine($"PG-ob：{ppp["ob"]}");
                sb.AppendLine($"PG-NUM：{ppp["NUM"]}");
                sb.AppendLine($"PG-NUMMO：{ppp["NUMMO"]}");
                sb.AppendLine($"PG-NUMR：{Math.Round((ppp["NUMMO"] is IConvertible)? ((IConvertible)ppp["NUMMO"]).ToDouble(null) :-1.0d)}");
            }

            txtNodeInfo.Text = sb.ToString();
            selN = e.Node;
        }

        // 根據點選 TreeNode CheckBox 狀態，來變成子節點
        private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode ChildeNode in e.Node.Nodes)
            {
                ChildeNode.Checked = e.Node.Checked;
            }
        }

        private TreeNode selN = new TreeNode();

        // 對應資料來源 DataTable 的欄位索引
        private int IDColIndex = 0;
        private int ParentIDColIndex = 1;
        private int TextColIndex = 2;
        private int pageIndex = 3;

        private void TreeBuild(DataTable dt)
        {
            TreeRootExist(dt);
            CreateRootNode(this.treeView1, dt);
        }

        private void TreeRootExist(DataTable dt)
        {
            EnumerableRowCollection<DataRow> result = dt
                .AsEnumerable()
                .Where(r => r.Field<string>(this.ParentIDColIndex) == null);

            if (result.Any() == false)
                throw new Exception("沒有 Root 節點資料，無法建立 TreeView");

            if (result.Count() > 1)
                throw new Exception("Root 節點超過 1 個，無法建立 TreeView");
        }

        private DataRow GetTreeRoot(DataTable dt)
        {
            return dt.AsEnumerable()
                .Where(r => r.Field<string>(this.ParentIDColIndex) == null)
                .First();
        }

        private IEnumerable<DataRow> GetTreeNodes(DataTable dt, TreeNode Node)
        {
            return dt.AsEnumerable()
                .Where(r => r.Field<string>(this.ParentIDColIndex) == Node.Name)
                .OrderBy(r => r.Field<string>(this.IDColIndex));
        }

        private void CreateRootNode(TreeView tree, DataTable dt)
        {
            DataRow Root = GetTreeRoot(dt);
            TreeNode Node = new TreeNode();
            Node.Text = Root.Field<string>(this.TextColIndex);
            Node.Name = Root.Field<string>(this.IDColIndex);
            Node.Tag = "根節點";
            tree.Nodes.Add(Node);

            CreateNode(tree, dt, Node);
        }

        private void CreateNode(TreeView tree, DataTable dt, TreeNode Node)
        {
            IEnumerable<DataRow> Rows = GetTreeNodes(dt, Node);

            TreeNode NewNode;
            foreach (DataRow r in Rows)
            {
                NewNode = new TreeNode();
                NewNode.Name = r.Field<string>(this.IDColIndex);
                NewNode.Text = r.Field<string>(this.TextColIndex);
                NewNode.Tag = r.Field<string>(this.pageIndex);
                Node.Nodes.Add(NewNode);

                CreateNode(tree, dt, NewNode);
            }
        }

        private DataTable GetTreeData()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("DepID", typeof(string));
            dt.Columns.Add("ParentID", typeof(string));
            dt.Columns.Add("DepName", typeof(string));

            dt.Rows.Add("01", null, "根結點");
            dt.Rows.Add("02", "01", "財務");
            dt.Rows.Add("03", "01", "行政");
            dt.Rows.Add("04", "03", "採購");
            dt.Rows.Add("05", "03", "人資");
            dt.Rows.Add("06", "03", "業務");
            dt.Rows.Add("07", "03", "技術服務");
            dt.Rows.Add("08", "01", "開發");
            dt.Rows.Add("09", "08", "企劃");
            dt.Rows.Add("10", "08", "品管");
            dt.Rows.Add("11", "01", "廠務");
            dt.Rows.Add("12", "11", "生產技術");
            dt.Rows.Add("13", "11", "製程保全");
            dt.Rows.Add("14", "11", "生產管理");
            dt.Rows.Add("15", "11", "廠務室 A");
            dt.Rows.Add("16", "15", "倉庫 A");
            dt.Rows.Add("17", "15", "板噴生產課");
            dt.Rows.Add("18", "17", "端板成型組");
            dt.Rows.Add("19", "17", "箱體塗裝組");
            dt.Rows.Add("20", "15", "熱交生產課");
            dt.Rows.Add("21", "20", "管件組");
            dt.Rows.Add("22", "20", "沖片組");
            dt.Rows.Add("23", "20", "回管組");
            dt.Rows.Add("24", "20", "氣焊組");
            dt.Rows.Add("25", "15", "裝配生產課");
            dt.Rows.Add("26", "25", "裝配一組");
            dt.Rows.Add("27", "25", "裝配二組");
            dt.Rows.Add("28", "11", "廠務室 B");
            dt.Rows.Add("29", "28", "倉庫 B");
            dt.Rows.Add("30", "28", "資材課");
            dt.Rows.Add("31", "28", "板金生產課");
            dt.Rows.Add("32", "31", "CNC 組");

            return dt;
        }
        PdfDocument document = null;
        PdfFont font = null;
        PdfBrush brush = null;
        float yPos;
        private void button1_Click(object sender, EventArgs e)
        {
            document = new PdfDocument();
            font = new PdfStandardFont(PdfFontFamily.Helvetica, 10f);
            brush = new PdfSolidBrush(Color.Black);
            PdfSection SectionTOC = document.Sections.Add();
            PdfPage pageTOC = SectionTOC.Pages.Add();
            PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
            pageTOC.Graphics.DrawString("Table Of Contents", font, brush, new RectangleF(PointF.Empty, new SizeF(pageTOC.Graphics.ClientSize.Width, 20)), format);
            PdfSection SectionContent = document.Sections.Add();
            yPos = 30;
            for (int i = 1; i <= 2; i++)
            {
                PdfPage pageContent = SectionContent.Pages.Add();
                //Add bookmark in PDF document
                PdfBookmark bookmark = AddBookmark(pageContent, pageTOC, "Chapter " + i, new PointF(10, 30));
                //Add sections to bookmark
                PdfBookmark section1 = AddSection(bookmark, pageContent, pageTOC, "Section " + i + ".1", new PointF(30, 50), false);
                //Add subsections to section
                PdfBookmark subsection1 = AddSection(section1, pageContent, pageTOC, "Paragraph " + i + ".1.1", new PointF(50, 70), true);
                PdfBookmark subsection2 = AddSection(section1, pageContent, pageTOC, "Paragraph " + i + ".1.2", new PointF(50, 170), true);
                PdfBookmark subsection3 = AddSection(section1, pageContent, pageTOC, "Paragraph " + i + ".1.3", new PointF(50, 270), true);
                PdfBookmark section2 = AddSection(bookmark, pageContent, pageTOC, "Section " + i + ".2", new PointF(30, 420), false);
                PdfBookmark subsection4 = AddSection(section2, pageContent, pageTOC, "Paragraph " + i + ".2.1", new PointF(50, 440), true);
                PdfBookmark subsection5 = AddSection(section2, pageContent, pageTOC, "Paragraph " + i + ".2.2", new PointF(50, 570), true);
                PdfBookmark subsection6 = AddSection(section2, pageContent, pageTOC, "Paragraph " + i + ".2.3", new PointF(50, 680), true);
            }
            document.Save("TableOfContents.pdf");
            document.Close(true);
            System.Diagnostics.Process.Start("TableOfContents.pdf");
        }
        public PdfBookmark AddBookmark(PdfPage page, PdfPage toc, string title, PointF point)
        {
            PdfGraphics graphics = page.Graphics;
            //Add bookmark in PDF document
            PdfBookmark bookmarks = document.Bookmarks.Add(title);
            //Draw the content in the PDF page
            graphics.DrawString(title, font, brush, new PointF(point.X, point.Y));
            //Add table of contents
            AddTableOfcontents(page, toc, title, point,"XXX");
            //Adding bookmark with named destination
            PdfNamedDestination namedDestination = new PdfNamedDestination(title);
            namedDestination.Destination = new PdfDestination(page, new PointF(point.X, point.Y));
            namedDestination.Destination.Mode = PdfDestinationMode.FitToPage;
            document.NamedDestinationCollection.Add(namedDestination);
            bookmarks.NamedDestination = namedDestination;
            return bookmarks;
        }
        public PdfBookmark AddSection(PdfBookmark bookmark, PdfPage page, PdfPage toc, string title, PointF point, bool isSubSection)
        {
            PdfGraphics graphics = page.Graphics;
            //Add bookmark in PDF document
            PdfBookmark bookmarks = bookmark.Add(title);
            //Draw the content in the PDF page
            graphics.DrawString(title, font, brush, new PointF(point.X, point.Y));
            //Add table of contents
            AddTableOfcontents(page, toc, title, point,"XXX");
            //Adding bookmark with named destination
            PdfNamedDestination namedDestination = new PdfNamedDestination(title);
            namedDestination.Destination = new PdfDestination(page, new PointF(point.X, point.Y));
            if (isSubSection == true)
                namedDestination.Destination.Zoom = 2f;
            else
                namedDestination.Destination.Zoom = 1f;
            document.NamedDestinationCollection.Add(namedDestination);
            bookmarks.NamedDestination = namedDestination;
            return bookmarks;
        }
        public void AddTableOfcontents(PdfPage page, PdfPage toc, string title, PointF point,String pageNUMBasSTRING)
        {
            //Draw title in TOC
            PdfTextElement element = new PdfTextElement(title, font, PdfBrushes.Blue);
            //Set layout format for pagination of TOC
            PdfLayoutFormat format = new PdfLayoutFormat();
            format.Break = PdfLayoutBreakType.FitPage;
            format.Layout = PdfLayoutType.Paginate;
            PdfLayoutResult result = element.Draw(toc, new PointF(point.X, yPos), format);
            //Draw page number in TOC
            PdfTextElement pageNumber = new PdfTextElement(pageNUMBasSTRING,//document.Pages.IndexOf(page).ToString(),
                font, brush);
            pageNumber.Draw(toc, new PointF(toc.Graphics.ClientSize.Width - 100, yPos));
            //Creates a new document link annotation.
            RectangleF bounds = result.Bounds;
            bounds.Width = toc.Graphics.ClientSize.Width - point.X;
            PdfDocumentLinkAnnotation documentLinkAnnotation = new PdfDocumentLinkAnnotation(bounds);
            documentLinkAnnotation.AnnotationFlags = PdfAnnotationFlags.NoRotate;
            documentLinkAnnotation.Text = title;
            documentLinkAnnotation.Color = Color.Transparent;
            //Sets the destination.
            documentLinkAnnotation.Destination = new PdfDestination(page);
            documentLinkAnnotation.Destination.Location = point;
            //Adds this annotation to a new page.
            toc.Annotations.Add(documentLinkAnnotation);
            if (toc != result.Page)
            {
                yPos = result.Bounds.Height + 5;
            }
            else
            {
                yPos += result.Bounds.Height + 5;
            }
            toc = result.Page;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file";
            dialog.InitialDirectory = ".\\";
            dialog.Filter = "pdf files (*.*)|*.pdf";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(dialog.FileName);
                label2.Text = dialog.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            iTextSharp.text.pdf.PdfReader pdfReader = new iTextSharp.text.pdf.PdfReader(label2.Text);

            IList<Dictionary<string, object>> bookmarks = iTextSharp.text.pdf.SimpleBookmark.GetBookmark(pdfReader);

           /* for (int i = 0; i < bookmarks.Count; i++)
            {
                MessageBox.Show(bookmarks[i].Values.ToArray().GetValue(0).ToString());

                if (bookmarks[i].Count > 3)
                {
                    MessageBox.Show(bookmarks[i].ToList().Count.ToString());
                }
            }*/


            document = new PdfDocument();
            font = new PdfStandardFont(PdfFontFamily.Helvetica, 10f);
            brush = new PdfSolidBrush(Color.Black);
            PdfSection SectionTOC = document.Sections.Add();
            PdfPage pageTOC = SectionTOC.Pages.Add();
            PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
            String TOCTTL = "TableOfContents" + DateTime.Now.ToString(@"__MM_dd_yyyy__hh_mm_ss_tt") + ".pdf";
            pageTOC.Graphics.DrawString(label2.Text, font, brush, new RectangleF(PointF.Empty, new SizeF(pageTOC.Graphics.ClientSize.Width, 20)), format);
            PdfSection SectionContent = document.Sections.Add();
            yPos = 30;
            //int c48 = 0;
            for (int i = 0; i < bookmarks.Count; i++)
            {/*
                PdfPage pageContent = SectionContent.Pages.Add();
                //Add bookmark in PDF document
                PdfBookmark bookmark = AddBookmark(pageContent, pageTOC, "Chapter " + i+
                    " > "+ bookmarks[i].Values.ToArray().GetValue(0).ToString(), new PointF(10, 30));
                //Add sections to bookmark
                PdfBookmark section1 = AddSection(bookmark, pageContent, pageTOC, "Section " + i + ".1", new PointF(30, 50), false);
                //Add subsections to section
                PdfBookmark subsection1 = AddSection(section1, pageContent, pageTOC, "Paragraph " + i + ".1.1", new PointF(50, 70), true);
                PdfBookmark subsection2 = AddSection(section1, pageContent, pageTOC, "Paragraph " + i + ".1.2", new PointF(50, 170), true);
                PdfBookmark subsection3 = AddSection(section1, pageContent, pageTOC, "Paragraph " + i + ".1.3", new PointF(50, 270), true);
                PdfBookmark section2 = AddSection(bookmark, pageContent, pageTOC, "Section " + i + ".2", new PointF(30, 420), false);
                PdfBookmark subsection4 = AddSection(section2, pageContent, pageTOC, "Paragraph " + i + ".2.1", new PointF(50, 440), true);
                PdfBookmark subsection5 = AddSection(section2, pageContent, pageTOC, "Paragraph " + i + ".2.2", new PointF(50, 570), true);
                PdfBookmark subsection6 = AddSection(section2, pageContent, pageTOC, "Paragraph " + i + ".2.3", new PointF(50, 680), true);
                */
                PdfPage pageContent = SectionContent.Pages.Add();
                String pgnot = bookmarks[i].Values.ToArray().Length>0? bookmarks[i].Values.ToArray().GetValue(0).ToString():"ERR";
                String pgnos = bookmarks[i].Values.ToArray().Length > 1 ? bookmarks[i].Values.ToArray().GetValue(1).ToString() : "ERR";
                String pgno1 = new String(pgnos.TakeWhile(Char.IsDigit).ToArray());
                int number = 0;
                bool conversionSuccessful = int.TryParse(pgno1, out number);
                AddTableOfcontents(pageContent, pageTOC, "Chapter " + i +
                    " > " + pgnot, 
                    new PointF(10, 30),
                    pgnos + " ["+ (conversionSuccessful? (number/2).ToString() :"ERR")+ "]");
                /*if (c48 > 48)
                {
                    pageTOC = SectionTOC.Pages.Add();
                    c48 = 0;
                }
                else
                {
                    c48++;
                }*/
                if (bookmarks[i].Count > 3)
                {
                    var f113 = bookmarks[i].ElementAt(3).Value;
                    // MessageBox.Show(bookmarks[i].ToList().Count.ToString());
                    l2 i2 = new l2();
                    IEnumerable myList = f113 as IEnumerable;
                    if (myList != null)
                    {
                        if (checkBox1.Checked) {
                            foreach (object element in myList)
                            {
                                Dictionary<String, Object> www = element as Dictionary<String, Object>;
                                String pgnot2 = www.Values.ToArray().GetValue(0).ToString();
                                String pgnos2 = www.Values.ToArray().GetValue(1).ToString();
                                i2.add(pgnot2, pgnos2);
                            }
                            foreach (KeyValuePair<string,string> i2g in i2.get())
                            {
                                // ... do something
                                PdfPage pageContent2 = SectionContent.Pages.Add();
                                AddTableOfcontents(pageContent, pageTOC, "Section " + 0 +
                        " > " + i2g.Key,
                        new PointF(30, 50),
                        i2g.Value);

                            }
                        }

                        else
                        {
                            foreach (object element in myList)
                            {
                                // ... do something
                                Dictionary<String, Object> www = element as Dictionary<String, Object>;
                                PdfPage pageContent2 = SectionContent.Pages.Add();
                                String pgnot2 = www.Values.ToArray().GetValue(0).ToString();
                                String pgnos2 = www.Values.ToArray().GetValue(1).ToString();
                                String pgno12 = new String(pgnos2.TakeWhile(Char.IsDigit).ToArray());
                                int number2 = 0;
                                bool conversionSuccessful2 = int.TryParse(pgno12, out number2);
                                AddTableOfcontents(pageContent, pageTOC, "Section " + 0 +
                        " > " + pgnot2,
                        new PointF(30, 50), pgnos2 + " [" + (conversionSuccessful2 ? (number2 / 2).ToString() : "ERR") + "]");
                                /*  if (c48 > 48) {
                                      pageTOC = SectionTOC.Pages.Add();
                                      c48 = 0;
                                  } else {
                                      c48++;
                                  }*/
                            }
                        }
                    }
                    else
                    {
                        // it's not an array, list, ...
                        MessageBox.Show("ERR");
                    }
                    
                }
            }
            document.Save(TOCTTL);
            document.Close(true);
            System.Diagnostics.Process.Start(TOCTTL);


        }

        private void button4_Click(object sender, EventArgs e)
        {

            DataTable dt = GetTreeData();
            TreeBuild(dt);

            treeView1.ExpandAll();
            treeView1.AfterSelect += TreeView1_AfterSelect;
            treeView1.AfterCheck += TreeView1_AfterCheck;

            FocusOnRoot();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            DataTable dt = GetTreeData2();
            TreeBuild(dt);

            treeView1.ExpandAll();
            treeView1.AfterSelect += TreeView1_AfterSelect;
            treeView1.AfterCheck += TreeView1_AfterCheck;

            FocusOnRoot();

            }

        private DataTable GetTreeData2() {

            DataTable dt = new DataTable();
            dt.Columns.Add("DepID", typeof(string));
            dt.Columns.Add("ParentID", typeof(string));
            dt.Columns.Add("DepName", typeof(string));
            dt.Columns.Add("pg", typeof(string));

            dt.Rows.Add("0", null, label2.Text, "根節點");
            iTextSharp.text.pdf.PdfReader pdfReader = new iTextSharp.text.pdf.PdfReader(label2.Text);

            IList<Dictionary<string, object>> bookmarks = iTextSharp.text.pdf.SimpleBookmark.GetBookmark(pdfReader);
            Dictionary<int, IList<Dictionary<string, object>>> var_tmp_BMS0 = new Dictionary<int, IList<Dictionary<string, object>>>();
            var_tmp_BMS0.Add(0, bookmarks);
            BMS(var_tmp_BMS0, dt);
            return dt;
        }
        private DataTable pgdt = new DataTable();

        private void pgdtAdd(int ix,string pg) {
            if (pg == "N/A") {
                pgdt.Rows.Add(ix.ToString(), pg,  "N/A" , false, -1,-1.0);
} else {
                String pgno1 = new String(pg.TakeWhile(Char.IsDigit).ToArray());
            int number = -1;
            bool conversionSuccessful = int.TryParse(pgno1, out number);
                pgdt.Rows.Add(ix.ToString(), pg, pgno1, conversionSuccessful, number, (number * numericUpDown1.Value) + numericUpDown2.Value);

            }
        }
        private Dictionary<int, IList<Dictionary<string, object>>> BMS(Dictionary<int,IList<Dictionary<string, object>>> ob,DataTable dt) {
            Dictionary<int, IList<Dictionary<string, object>>> tmp = new Dictionary<int, IList<Dictionary<string, object>>>();
            
            if (ob.Count != 0)
            {
                foreach (KeyValuePair<int, IList<Dictionary<string, object>>> bn in ob)
                {
                    foreach (Dictionary<string, object> sb in bn.Value)
                    {
                        int var_this_index = dt.Rows.Count;
                        string pgy = sb.ContainsKey("Page") ? sb["Page"] as string: "N/A";
                        dt.Rows.Add(var_this_index.ToString(), bn.Key.ToString(), sb["Title"], pgy);
                        pgdtAdd(var_this_index, pgy);
                        if (sb.ContainsKey("Kids"))
                        {
                            IList<Dictionary<string, object>> kids = sb["Kids"] as IList<Dictionary<string, object>>;
                            if (!(kids is null))
                            {
                                tmp.Add(var_this_index, kids);
                            }
                        }
                    }
                }
                return BMS(tmp, dt);
            } else { return null; }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Remove(selN);//.RemoveByKey(label3.Text);
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
                  label3.Text = Regex.Replace(selN.Text, @"[\d-]", string.Empty);
            List<TreeNode> rm = new List<TreeNode>();
            CallNonRecursive(treeView1,rm,label3.Text);
            foreach (TreeNode el in rm) {
                treeView1.Nodes.Remove(el);
            }
        }

        private void PrintNonRecursive(TreeNode treeNode, List<TreeNode> pt, String target)
        {
            if (treeNode != null)
            {
                //Using a queue to store and process each node in the TreeView
                Queue<TreeNode> staging = new Queue<TreeNode>();
                staging.Enqueue(treeNode);

                while (staging.Count > 0)
                {
                    treeNode = staging.Dequeue();

                    // Print the node.  
                    //System.Diagnostics.Debug.WriteLine(treeNode.Text);
                    //MessageBox.Show(treeNode.Text);
                    if (treeNode.Text.Contains(target)) { pt.Add(treeNode); }

                    foreach (TreeNode node in treeNode.Nodes)
                    {
                        staging.Enqueue(node);
                    }
                }
            }
        }

        // Call the procedure using the TreeView.  
        private void CallNonRecursive(TreeView treeView,List<TreeNode> pt,String target)
        {
            // Print each node.
            foreach (TreeNode n in treeView.Nodes)
            {
                PrintNonRecursive(n, pt, target);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CallRecursive(treeView1);
            foreach (KeyValuePair<TreeNode, List<TreeNode>> el in rmNear) {
                el.Key.Tag += "~" + el.Value.Last().Tag;
                foreach (TreeNode elm in el.Value) {
                    treeView1.Nodes.Remove(elm);
                }
            }
        }

        private Dictionary<int, TreeNode> hierarchicalBypassEquivalentRemovingStack = new Dictionary<int, TreeNode>();
        private Dictionary<TreeNode,List<TreeNode>> rmNear = new Dictionary<TreeNode, List<TreeNode>>();

        private void PrintRecursive(TreeNode treeNode)
        {
            // Print the node.  
            //System.Diagnostics.Debug.WriteLine(treeNode.Text);
            //MessageBox.Show(treeNode.Text);
            if (hierarchicalBypassEquivalentRemovingStack.ContainsKey(treeNode.Level)) {
               if(treeNode.Text== hierarchicalBypassEquivalentRemovingStack[treeNode.Level].Text)
                {
                    if (rmNear.ContainsKey(hierarchicalBypassEquivalentRemovingStack[treeNode.Level])) {
                        rmNear[hierarchicalBypassEquivalentRemovingStack[treeNode.Level]].Add(treeNode);
                    } else {
                        List<TreeNode> tmpltn = new List<TreeNode>();
                        tmpltn.Add(treeNode);
                        rmNear.Add(hierarchicalBypassEquivalentRemovingStack[treeNode.Level], tmpltn);
                    }
                }
                else
                {
                    hierarchicalBypassEquivalentRemovingStack[treeNode.Level] = treeNode;
                }
            } else {
                hierarchicalBypassEquivalentRemovingStack.Add(treeNode.Level, treeNode);
            }
            label1.Text += treeNode.Level;

            // Visit each node recursively.  
            foreach (TreeNode tn in treeNode.Nodes)
            {
                PrintRecursive(tn);
            }
        }

        // Call the procedure using the TreeView.  
        /**
         * 深度優先
         */
        private void CallRecursive(TreeView treeView)
        {
            // Print each node recursively.  
            foreach (TreeNode n in treeView.Nodes)
            {
                //recursiveTotalNodes++;
                PrintRecursive(n);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            String TOCTTL = "SNAP" + DateTime.Now.ToString(@"__MM_dd_yyyy__hh_mm_ss_tt") + ".gif";
            var bm = new Bitmap(Width, Height);
            treeView1.DrawToBitmap(bm, treeView1.Bounds);
            bm.Save(TOCTTL, ImageFormat.Gif);
        }

        private void pictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button9.PerformClick();

        }

        private void loadPdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.PerformClick();
        }

        private void loadTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button5.PerformClick();
        }

        private void removeThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button6.PerformClick();
        }

        private void removeSimilarIndividualsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button7.PerformClick();
        }

        private void removeSamelayerEquivalentNeighborToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button8.PerformClick();
        }

        private void treeViewTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = GetTreeViewScrollPos(treeView1).ToString();
        }

        /* A method which returns a point for the current scroll position:
         * https://stackoverflow.com/questions/332788/maintain-scroll-position-of-treeview
        */
        private Point GetTreeViewScrollPos(TreeView treeView)
        {
            return new Point(
                GetScrollPos(treeView.Handle, SB_HORZ),
                GetScrollPos(treeView.Handle, SB_VERT));
        }

        private void yToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point ptmp = new Point(0, 25);
            SetTreeViewScrollPos(treeView1, ptmp);
        }

        /*A method to set the scroll position:
        */
private void SetTreeViewScrollPos(TreeView treeView, Point scrollPosition)
        {
            SetScrollPos(treeView.Handle, SB_HORZ, scrollPosition.X, true);
            SetScrollPos(treeView.Handle, SB_VERT, scrollPosition.Y, true);
        }

        private void downToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = treeView1.GetNodeCount(true).ToString();
           //    Point ptmp = new Point(0, treeView1.SelectedNode.Index+1);
          //  treeView1.SelectedNode = treeView1.GetNodeAt();
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void scrollToBotomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.Nodes[treeView1.Nodes.Count - 1].EnsureVisible();
            treeView1.Refresh();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
          //  treeView1.lay
        }

        private void tPDF3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.PerformClick();
        }
        private void PrintRecursive2(TreeNode treeNode)
        {
            // do something here...
            if (settingCKi()<=0||treeNode.Level<= settingCKi()) {
                DataRow ppp = pgdt.Rows.Find(treeNode.Name);
                string tmpPP = treeNode.Tag.ToString();
                if (!(ppp is null))//TODO SEL
                {
                    tmpPP += "/" + (Math.Round((ppp["NUMMO"] is IConvertible) ? ((IConvertible)ppp["NUMMO"]).ToDouble(null) : -1.0d).ToString());
                    /*sb.AppendLine($"PG-CustID_：{ppp["CustID_"]}");
                    sb.AppendLine($"PG-ORG：{ppp["ORG"]}");
                    sb.AppendLine($"PG-numS：{ppp["numS"]}");
                    sb.AppendLine($"PG-ob：{ppp["ob"]}");
                    sb.AppendLine($"PG-NUM：{ppp["NUM"]}");
                    sb.AppendLine($"PG-NUMMO：{ppp["NUMMO"]}");
                    sb.AppendLine($"PG-NUMR：{Math.Round((ppp["NUMMO"] is IConvertible) ? ((IConvertible)ppp["NUMMO"]).ToDouble(null) : -1.0d)}");
              */
                }
                pt01.add(treeNode.Text, tmpPP, treeNode.Level);
            }
            //!!

            // Visit each node recursively.  
            foreach (TreeNode tn in treeNode.Nodes)
            {
                PrintRecursive2(tn);
            }
        }

        // Call the procedure using the TreeView.  
        private void CallRecursive2(TreeView treeView)
        {
            // Print each node recursively.  
            foreach (TreeNode n in treeView.Nodes)
            {
                //recursiveTotalNodes++;
                PrintRecursive2(n);
            }
        }

    /*    private void PrintNonRecursive2(TreeNode treeNode)
        {
            if (treeNode != null)
            {
                //Using a queue to store and process each node in the TreeView
                Queue<TreeNode> staging = new Queue<TreeNode>();
                staging.Enqueue(treeNode);

                while (staging.Count > 0)
                {
                    treeNode = staging.Dequeue();

                    // do something here...
                    pt01.add(treeNode.Text, treeNode.Tag.ToString(), treeNode.Level);
                    //!!

                    foreach (TreeNode node in treeNode.Nodes)
                    {
                        staging.Enqueue(node);
                    }
                }
            }
        }

        // Call the procedure using the TreeView.  
        private void CallNonRecursive2(TreeView treeView)
        {
            // Print each node.
            foreach (TreeNode n in treeView.Nodes)
            {
                PrintNonRecursive2(n);
            }
        }*/
        private pdftoc pt01 = null;
        private void pdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pt01 = new pdftoc("WWW");
            CallRecursive2(treeView1);
            string tmpfn = pt01.finish();
            //TODO rm old
            removeBlankPdfPages(tmpfn,
                "RE_TableOfContents" + DateTime.Now.ToString(@"__MM_dd_yyyy__hh_mm_ss_tt") + ".pdf",
             removeBlankPageToolStripMenuItem1.Checked   );
        }
        public static void removeBlankPdfPages(string pdfSourceFile, string pdfDestinationFile, bool debug)
        {

            // step 0: set minimum page size
            const int blankPdfsize = 20;

            // step 1: create new reader
            var r = new iTextSharp.text.pdf.PdfReader(pdfSourceFile);
            var raf = new iTextSharp.text.pdf.RandomAccessFileOrArray(pdfSourceFile);
            var document = new iTextSharp.text.Document(r.GetPageSizeWithRotation(1));

            // step 2: create a writer that listens to the document
            var writer = new iTextSharp.text.pdf.PdfCopy(document, new FileStream(pdfDestinationFile, FileMode.Create));

            // step 3: we open the document
            document.Open();

            // step 4: we add content
            iTextSharp.text.pdf.PdfImportedPage page = null;

            //loop through each page and if the bs is larger than 20 than we know it is not blank.
            //if it is less than 20 than we don't include that blank page.
            for (var i = 1; i <= r.NumberOfPages; i++)
            {
                //get the page content
                byte[] bContent = r.GetPageContent(i, raf);
                var bs = new MemoryStream();

                //write the content to an output stream
                bs.Write(bContent, 0, bContent.Length);
                Console.WriteLine("page content length of page {0} = {1}", i, bs.Length);

                //add the page to the new pdf
                if (bs.Length > blankPdfsize)
                {
                    page = writer.GetImportedPage(r, i);
                    writer.AddPage(page);
                }
                bs.Close();
            }
            //close everything
            document.Close();
            writer.Close();
            raf.Close();
            r.Close();
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
                        settingSetNull();
            toolStripMenuItem3.Checked = true; settingCK();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            settingSetNull(); toolStripMenuItem4.Checked = true; settingCK();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            settingSetNull(); toolStripMenuItem5.Checked = true; settingCK();
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingSetNull(); allToolStripMenuItem.Checked = true;
            settingCK();
        }

        private void settingSetNull() {
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = false;
            allToolStripMenuItem.Checked = false;
        }
        private void settingCK()
        {
            if (toolStripMenuItem3.Checked) {
                toolStripStatusLabel3.Text = "1";
            }
            else if (
toolStripMenuItem4.Checked) { toolStripStatusLabel3.Text = "2"; }
            else if (
toolStripMenuItem5.Checked) { toolStripStatusLabel3.Text = "3"; }
            else if (
allToolStripMenuItem.Checked) { toolStripStatusLabel3.Text = "0"; }
            else { toolStripStatusLabel3.Text = "-1"; }
        }
        private int settingCKi()
        {
            int right = 0;  //Or you may want to set it to some other default value

            if (!int.TryParse(toolStripStatusLabel3.Text.Trim(), out right))
            {
                // Do some error handling here.. Maybe tell the user that data is invalid.
                 right = -1;
            }
            return right;
            // do the rest of your coding..  
        }

        private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
