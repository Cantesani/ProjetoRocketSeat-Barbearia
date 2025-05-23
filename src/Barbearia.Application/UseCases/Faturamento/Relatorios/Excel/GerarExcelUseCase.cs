using Barbearia.Domain.Enums.Extensions;
using Barbearia.Domain.Repositories;
using Barbearia.Domain.Resources;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Globalization;

namespace Barbearia.Application.UseCases.Faturamento.Relatorios.Excel
{
    public class GerarExcelUseCase : IGerarExcelUseCase
    {
        private readonly IFaturamentoReadOnlyRepository _repository;

        public GerarExcelUseCase(IFaturamentoReadOnlyRepository repository)
        {
            _repository = repository;
        }

        public async Task<byte[]> Execute(DateOnly mes)
        {
            var faturamentos = await _repository.GetByMes(mes);

            if (faturamentos.Count() == 0)
                return [];

            using var workBook = new XLWorkbook();

            workBook.Author = "Gabriel";
            workBook.Style.Font.FontSize = 12;
            workBook.Style.Font.FontName = "Times New Roman";

            var workSheet = workBook.Worksheets.Add(mes.ToString("Y"));

            InsereHeader(workSheet);

            var contador = 2;
            foreach (var faturamento in faturamentos)
            {
                workSheet.Cell($"A{contador}").Value = faturamento.Titulo;
                workSheet.Cell($"B{contador}").Value = faturamento.Data.ToString("dd/MM/yyyy", new CultureInfo("pt-BR"));
                workSheet.Cell($"C{contador}").Value = faturamento.TipoPagto.TipoPagtoToString();
                workSheet.Cell($"D{contador}").Value = faturamento.Valor.ToString("C", new CultureInfo("pt-BR"));
                workSheet.Cell($"E{contador}").Value = faturamento.Descricao;

                contador++;
            }

            var file = new MemoryStream();
            workBook.SaveAs(file);

            return file.ToArray();
        }

        public void InsereHeader(IXLWorksheet sheet)
        {
            //TITULOS
            sheet.Cell("A1").Value = ResourcesGerarMessages.TITULO;
            sheet.Cell("B1").Value = ResourcesGerarMessages.DATA;
            sheet.Cell("C1").Value = ResourcesGerarMessages.TIPO_PAGTO;
            sheet.Cell("D1").Value = ResourcesGerarMessages.VALOR;
            sheet.Cell("E1").Value = ResourcesGerarMessages.DESCRICAO;

            //STYLE
            sheet.Cells("A1:E1").Style.Font.Bold = true;
            sheet.Cells("A1:E1").Style.Font.FontColor = XLColor.FromHtml("#FFFFFF");
            sheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#205858");
            sheet.Cells("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            sheet.Cells("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            sheet.Cells("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            sheet.Cells("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
            sheet.Cells("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

        }
    }
}
