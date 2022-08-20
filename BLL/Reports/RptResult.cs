using ComprasBlazor.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ComprasBlazor.BLL.Reports
{
    public class RptResult : PdfFooterPart
    {
        PdfWriter _pdfWriter;
        int _maxColumn = 4;
        Document _document;
        PdfPTable _pdfPTable = new PdfPTable(8);
        PdfCell _pdfCell;
        Font _fontStyle;
        MemoryStream _memoryStream = new MemoryStream();

        public byte[] Report(List<Productos> productos)
        {
            // _productos = productos;
            _document = new Document(PageSize.A4, 10f, 10f, 20f, 30f);
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);

            _pdfWriter = PdfWriter.GetInstance(_document, _memoryStream);
            _pdfWriter.PageEvent = new PdfFooterPart();

            _document.Open();

            Paragraph titlePricipal = new Paragraph();
            titlePricipal.Font = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14f);
            titlePricipal.Alignment = Element.ALIGN_CENTER;
            titlePricipal.Add("Lista De Productos");
            titlePricipal.SpacingAfter = 5;
            _document.Add(titlePricipal);

            Font fontHeader = new Font(Font.TIMES_ROMAN, 12, Font.BOLD, BaseColor.Black);
            Font fontNormal = new Font(Font.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.Black);

            //tabla detalle monto mensual
            PdfPTable tblLocal = new PdfPTable(4);
            tblLocal.WidthPercentage = 100;
            float[] widths = new float[] { 12f, 5f, 3f, 4f };
            tblLocal.SetWidths(widths);

            PdfPCell clProductoId = new PdfPCell(new Phrase("ProductoId", fontHeader));
            PdfPCell clDescripcion = new PdfPCell(new Phrase("Descripcion", fontHeader));
            PdfPCell clExistencia = new PdfPCell(new Phrase("Existencia", fontHeader));
            PdfPCell clCosto = new PdfPCell(new Phrase("Costo", fontHeader));
            PdfPCell clInventario = new PdfPCell(new Phrase("Valir Inventario", fontHeader));

            tblLocal.AddCell(clProductoId);
            tblLocal.AddCell(clDescripcion);
            tblLocal.AddCell(clExistencia);
            tblLocal.AddCell(clCosto);
            tblLocal.AddCell(clInventario);


            foreach (var _productos in productos)
            {
                PdfPCell cldProductoId = new PdfPCell(new Phrase(_productos.ProductoId.ToString(), fontNormal));

                PdfPCell cldNombre = new PdfPCell(new Phrase(_productos.Descripcion, fontNormal));

                PdfPCell cldTipo = new PdfPCell(new Phrase(_productos.Existencia.ToString(), fontNormal));

                PdfPCell cldnumerolocal = new PdfPCell(new Phrase(_productos.Costo.ToString(), fontNormal));

                PdfPCell cldMonto = new PdfPCell(new Phrase(_productos.Precio.ToString("C"), fontNormal));
                cldMonto.HorizontalAlignment = 2;

                PdfPCell cldInventario = new PdfPCell(new Phrase((_productos.Costo * _productos.Existencia).ToString("C"), fontNormal));
                cldInventario.HorizontalAlignment = 2;

                tblLocal.AddCell(cldProductoId);
                tblLocal.AddCell(cldNombre);
                tblLocal.AddCell(cldTipo);
                tblLocal.AddCell(cldnumerolocal);
                tblLocal.AddCell(cldMonto);
                tblLocal.AddCell(cldInventario);
            }

            _document.Add(tblLocal);

            this.OnEndPage(_pdfWriter, _document);
            _document.Close();

            return _memoryStream.ToArray();
        }
    }
}
