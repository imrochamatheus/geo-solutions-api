//using Microsoft.AspNetCore.Mvc;

//namespace GeoSolucoesAPI.Controllers
//{
//    public class OrcamentoController
//    {
//    }
//}

//[HttpPost("enviar")]
//public IActionResult EnviarOrcamento([FromBody] string emailUsuario)
//{
//    try
//    {
//        var pdfService = new PdfService();
//        var emailService = new EmailService();

//        byte[] pdf = pdfService.GerarOrcamentoPdfEmMemoria();
//        emailService.EnviarOrcamentoPorEmail(emailUsuario, pdf);

//        return Ok("Orçamento enviado com sucesso!");
//    }
//    catch (Exception ex)
//    {
//        return BadRequest($"Erro ao enviar orçamento: {ex.Message}");
//    }
//}