using Barbearia.Application.UseCases.Faturamento.Relatorios.Pdf.Colors;
using Barbearia.Application.UseCases.Faturamento.Relatorios.Pdf.Fonts;
using Barbearia.Domain.Enums.Extensions;
using Barbearia.Domain.Repositories;
using Barbearia.Domain.Resources;
using Barbearia.Exception;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using PdfSharp.Quality;
using System.Globalization;
using System.Reflection;

namespace Barbearia.Application.UseCases.Faturamento.Relatorios.Pdf
{
    public class GerarPdfUseCase : IGerarPdfUseCase
    {
        private readonly IFaturamentoReadOnlyRepository _repository;
        private const int HEIHT_LINHA_TABELA = 25;


        public GerarPdfUseCase(IFaturamentoReadOnlyRepository repository)
        {
            _repository = repository;
            GlobalFontSettings.FontResolver = new FaturamentoRelatorioFontResolver();
        }
        public async Task<byte[]> Execute(DateOnly mes)
        {
            var faturamentos = await _repository.GetByMes(mes);

            if (faturamentos is null)
                return [];

            var documento = CreateDocument(mes);
            var page = CreatePage(documento);

            CreateHeader(page);

            var totalFaturamento = faturamentos.Sum(x => x.Valor);

            CreateTotalFaturado(page, mes, totalFaturamento);

            foreach (var faturamento in faturamentos)
            {
                var tabela = CreateFaturamentoTable(page);

                var row = tabela.AddRow();
                row.Height = HEIHT_LINHA_TABELA;

                //adicionando primeira linha (cabeçalho) na tabela (noma do faturamento e valor.)
                AddHeaderTituloTablesFaturamento(row.Cells[0], faturamento.Titulo);
                AddHeaderValorTablesFaturamento(row.Cells[3]);

                //adiciona nova linha com tamanho padrao
                row = tabela.AddRow();
                row.Height = HEIHT_LINHA_TABELA;

                //preenche data do faturamento
                row.Cells[0].AddParagraph(faturamento.Data.ToString("D"));
                row.Cells[0].Format.LeftIndent = 20;
                StyleBaseFaturamento(row.Cells[0]);

                //preenche hora do faturamento
                row.Cells[1].AddParagraph(faturamento.Data.ToString("t"));
                StyleBaseFaturamento(row.Cells[1]);

                //preenche Tipo Pagto do faturamento
                row.Cells[2].AddParagraph(faturamento.TipoPagto.TipoPagtoToString());
                StyleBaseFaturamento(row.Cells[2]);

                //preenche valor da despesa
                AddBodyValorTablesFaturamento(row.Cells[3], faturamento.Valor);

                //preenche descricao da despesa caso houver

                if (!string.IsNullOrWhiteSpace(faturamento.Descricao))
                {
                    var rowDescricao = tabela.AddRow();
                    rowDescricao.Cells[0].AddParagraph(faturamento.Descricao);
                    rowDescricao.Cells[0].Format.Font = new Font { Name = FontHelpers.ROBOTO_REGULAR, Size = 9, Color = ColorsHelper.GRAY_DARK };
                    rowDescricao.Cells[0].Format.LeftIndent = 20;
                    rowDescricao.Cells[0].Shading.Color = ColorsHelper.GRAY_LIGTH;
                    rowDescricao.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                    rowDescricao.Cells[0].MergeRight = 2;
                    row.Cells[3].MergeDown = 1;
                }

                // Adiciona espaço em branco entre as tabelas
                AddEspacoBranco(tabela);
            }

            return RenderizarDocumento(documento);
        }

        public Document CreateDocument(DateOnly mes)
        {
            var documento = new Document();
            documento.Info.Author = "Gabriel Cantesani";

            var style = documento.Styles["Normal"];
            style!.Font.Name = FontHelpers.BEBASNEUE_REGULAR;

            return documento;
        }
        public Section CreatePage(Document documento)
        {
            var section = documento.AddSection();
            section.PageSetup = documento.DefaultPageSetup.Clone();

            section.PageSetup.PageFormat = PageFormat.A4;

            section.PageSetup.TopMargin = 53;
            section.PageSetup.LeftMargin = 40;
            section.PageSetup.RightMargin = 40;
            section.PageSetup.BottomMargin = 80;

            return section;
        }
        public void CreateHeader(Section page)
        {
            var tabela = page.AddTable();
            tabela.AddColumn();
            tabela.AddColumn("300");

            var linha = tabela.AddRow();

            var assembly = Assembly.GetExecutingAssembly();
            var diretorio = Path.GetDirectoryName(assembly.Location);
            var pathFile = Path.Combine(diretorio!, "Images", "logo_barbearia.png");

            linha.Cells[0].AddImage(pathFile);

            var nomeBarbearia = string.Format(ResourcesGerarMessages.BARBEARIA_DO, "GABRIEL");
            linha.Cells[1].AddParagraph().AddFormattedText(nomeBarbearia, new Font { Name = FontHelpers.BEBASNEUE_REGULAR, Size = 25 });
            linha.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
        }
        public void CreateTotalFaturado(Section page, DateOnly mes, decimal totalFaturamento)
        {
            var paragrafo = page.AddParagraph();

            paragrafo.Format.SpaceBefore = "40";
            paragrafo.Format.SpaceAfter = "40";

            var titulo = string.Format(ResourcesGerarMessages.FATURADO_EM, mes.ToString("Y"), new CultureInfo("pt-BR"));
            var valorFormatado = totalFaturamento.ToString("C", new CultureInfo("pt-BR"));

            paragrafo.AddFormattedText(titulo, new Font { Name = FontHelpers.ROBOTO_MEDIUM, Size = 15 });
            paragrafo.AddLineBreak();

            paragrafo.AddFormattedText(valorFormatado, new Font { Name = FontHelpers.BEBASNEUE_REGULAR, Size = 50 });

        }
        private Table CreateFaturamentoTable(Section page)
        {
            var tabela = page.AddTable();

            tabela.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
            tabela.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
            tabela.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
            tabela.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

            return tabela;
        }
        private void AddHeaderTituloTablesFaturamento(Cell cell, string tituloFaturamento)
        {
            cell.AddParagraph(tituloFaturamento);
            cell.Format.Font = new Font { Name = FontHelpers.BEBASNEUE_REGULAR, Size = 15, Color = ColorsHelper.WHITE };
            cell.Format.LeftIndent = 20;
            cell.Shading.Color = ColorsHelper.GREEN_DARK;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.MergeRight = 2;
        }
        private void AddHeaderValorTablesFaturamento(Cell cell)
        {
            cell.AddParagraph(ResourcesGerarMessages.VALOR);
            cell.Format.Font = new Font { Name = FontHelpers.BEBASNEUE_REGULAR, Size = 15, Color = ColorsHelper.WHITE };
            cell.Shading.Color = ColorsHelper.GREEN_MEDIUM;
            cell.VerticalAlignment = VerticalAlignment.Center;
        }
        private void StyleBaseFaturamento(Cell cell)
        {
            cell.Format.Font = new Font { Name = FontHelpers.ROBOTO_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
            cell.Shading.Color = ColorsHelper.GRAY_MEDIUM;
            cell.VerticalAlignment = VerticalAlignment.Center;
        }
        private void AddBodyValorTablesFaturamento(Cell cell, decimal valorDespesa)
        {
            cell.AddParagraph(valorDespesa.ToString("C", new CultureInfo("pt-BR")));
            cell.Format.Font = new Font { Name = FontHelpers.ROBOTO_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
            cell.Shading.Color = ColorsHelper.WHITE;
            cell.VerticalAlignment = VerticalAlignment.Center;
        }
        private void AddEspacoBranco(Table tabela)
        {
            var row = tabela.AddRow();
            row.Height = 30;
            row.Borders.Visible = false;
        }
        private byte[] RenderizarDocumento(Document documento)
        {
            var renderizar = new PdfDocumentRenderer
            {
                Document = documento
            };

            renderizar.RenderDocument();

            using var file = new MemoryStream();
            renderizar.PdfDocument.Save(file);

            return file.ToArray();
        }


    }
}
