using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateTOCFromBookmarks
{
    class pdftoc
    {

        // This constructor has no parameters. The parameterless constructor
        // is invoked in the processing of object initializers.
        // You can test this by changing the access modifier from public to
        // private. The declarations in Main that use object initializers will
        // fail.
        public pdftoc() { }
        public pdftoc(string filename)
        {
        font = new PdfStandardFont(PdfFontFamily.Helvetica, 10f);
        brush = new PdfSolidBrush(Color.Black);
         document = new PdfDocument();
         SectionTOC = document.Sections.Add();
         pageTOC = SectionTOC.Pages.Add();
        pageTOC.Graphics.DrawString(filename, font, brush, new RectangleF(PointF.Empty, new SizeF(pageTOC.Graphics.ClientSize.Width, 20)), format);
             SectionContent = document.Sections.Add();
        yPos = 30;
            cc.Add(0); cc.Add(0); cc.Add(0); cc.Add(0);//3+1
        }
       private PdfDocument document = null;
        private List<int> cc =new List<int>();
        private PdfFont font = null;
        private PdfBrush brush = null;
        private float yPos ;
        private PdfSection SectionTOC = null;
        private PdfPage pageTOC = null; PdfSection SectionContent = null;
        private PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
        private String TOCTTL = "TableOfContents" + DateTime.Now.ToString(@"__MM_dd_yyyy__hh_mm_ss_tt") + ".pdf";


        //**--
        private PdfBookmark AddBookmark(PdfPage page, PdfPage toc, string title, PointF point)
        {
            PdfGraphics graphics = page.Graphics;
            //Add bookmark in PDF document
            PdfBookmark bookmarks = document.Bookmarks.Add(title);
            //Draw the content in the PDF page
            graphics.DrawString(title, font, brush, new PointF(point.X, point.Y));
            //Add table of contents
            AddTableOfcontents(page, toc, title, point, "XXX");
            //Adding bookmark with named destination
            PdfNamedDestination namedDestination = new PdfNamedDestination(title);
            namedDestination.Destination = new PdfDestination(page, new PointF(point.X, point.Y));
            namedDestination.Destination.Mode = PdfDestinationMode.FitToPage;
            document.NamedDestinationCollection.Add(namedDestination);
            bookmarks.NamedDestination = namedDestination;
            return bookmarks;
        }
        private PdfBookmark AddSection(PdfBookmark bookmark, PdfPage page, PdfPage toc, string title, PointF point, bool isSubSection)
        {
            PdfGraphics graphics = page.Graphics;
            //Add bookmark in PDF document
            PdfBookmark bookmarks = bookmark.Add(title);
            //Draw the content in the PDF page
            graphics.DrawString(title, font, brush, new PointF(point.X, point.Y));
            //Add table of contents
            AddTableOfcontents(page, toc, title, point, "XXX");
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
        private void AddTableOfcontents(PdfPage page, PdfPage toc, string title, PointF point, String pageNUMBasSTRING)
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
        private string layerS(int l)
        {
            if (l == 1)
            {
                return "Chapter";
            }
            else if (l == 2)
            {
                return "Section";
            }
            else if (l == 3)
            {
                return "Paragraph";
            }
            else {
                return "_";
            }
        }
        private PointF layerP(int l)
        {
            if (l == 1)
            {
                return new PointF(10, (30+cc[0]-1)*20);
            }
            else if (l == 2)
            {
                return new PointF(30, (30 + cc[0] - 1) * 20);
            }
            else if (l == 3)
            {
                return new PointF(50, (30 + cc[0] - 1) * 20);
            }
            else
            {
                return new PointF(1, 1);
            }
        }

        public Boolean add(string aaa,string bbb, int layer)
        {
            if (cc[0]>43)
            {
                rst();
                cc[0] = 0;
            }
            if (layer == 0) {
                return false;
            }
            else
            {
                cc[layer+1]++;
                cc[0]++;
                PdfPage pageContent = SectionContent.Pages.Add();
                AddTableOfcontents(pageContent, pageTOC, layerS(layer) + cc[layer+1] +
                    " > " + aaa,
                    layerP(layer),
                    bbb);
                return true;
            }
        }
        public string finish() {
            document.Save(TOCTTL);
            document.Close(true);
            //System.Diagnostics.Process.Start(TOCTTL);
            return TOCTTL;
        }
        private void rst() {
            yPos = 30;
            pageTOC = SectionTOC.Pages.Add();
        }
    }//-x-
}
