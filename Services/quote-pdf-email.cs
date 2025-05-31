//using Npgsql.Internal;

//namespace GeoSolucoesAPI.Services
//{
//    public class quote_pdf_email
//    {
//    }
//}
// byte[] GerarOrcamentoPdfEmMemoria()
//{
//    using (MemoryStream ms = new MemoryStream())
//    {
//        Document doc = new Document(PageSize.A4);
//        PdfWriter.GetInstance(doc, ms);
//        doc.Open();

//        // Conteúdo do PDF (mesmo que já criamos)
//        doc.Add(new Paragraph("Orçamento"));
//        doc.Add(new Paragraph("Localização: Rua Etelvino Costa, Santo Antônio do Retiro, MG"));
//        doc.Add(new Paragraph("Complemento: Casa"));
//        doc.Add(new Paragraph("Área: 1200 ha"));
//        doc.Add(new Paragraph("Serviços: Georreferenciamento, Aerolevantamento"));
//        doc.Add(new Paragraph("Valor Final: R$ 2.829,40"));

//        doc.Close();
//        return ms.ToArray();
//    }
//}
