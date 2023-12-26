using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GenerarExcel.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        [HttpGet]
        [Route("PorCelda")]
        public IActionResult ExportExcel1()
        {

            try
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.AddWorksheet("Sample Sheet");
                worksheet.Cell("A1").Value = "Hello World!";
                worksheet.Cell("A2").FormulaA1 = "MID(A1, 7, 5)";

                using var memoria = new MemoryStream();
                workbook.SaveAs(memoria);
                var nombreExcel = "Reporte.xlsx";
                var archivo = File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                return archivo;

            }
            catch (Exception)
            {
                throw;

            }
        }

        [HttpGet]
        [Route("PorColumna")]
        public IActionResult ExportExcel2()
        {
            try
            {
                DataTable table = new DataTable();//tabla general

                table.Columns.Add("NAME");
                table.Columns.Add("ADRESS");
                table.Columns.Add("DATE");
                table.Columns.Add("INDICE");

                //Aqui van las filas y puedo añadir las filas que quiera o traerlas de una base de datos y con foreach interar

                DataRow fila = table.NewRow();
                fila["NAME"] = "Juan";
                fila["ADRESS"] = "Stret";
                fila["DATE"] = "23/12/2023";
                fila["INDICE"] = "Ok";


                table.Rows.Add(fila);


                using var libro = new XLWorkbook();
                table.TableName = "Registros";

                var hoja = libro.Worksheets.Add(table);

                hoja.ColumnsUsed().AdjustToContents();
                //agregar tablas de tanques al excel

                using var memoria = new MemoryStream();
                libro.SaveAs(memoria);
                var nombreExcel = "Reporte.xlsx";
                var archivo = File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                return archivo;
            }
            catch (Exception)
            {
                throw;

            }
        }


        [HttpGet]
        [Route("Template")]
        public IActionResult ExportExcel3()
        {
            try
            {
                using (var workbook = new XLWorkbook(@"C:\Users\Dagoberto\Documents\CursoDeBlazorWebassembly_DagoWasm\Proyects\GenerarExcel\Client\wwwroot\Nomina.xlsx"))
                {
                    var SampleSheet = workbook.Worksheets.Where(x => x.Name == "Empresa").First();

                    string CeldaItem = "C9";

                    //*************************************************

                    SampleSheet.Cell(CeldaItem).Value = 1800000;

                    using var memoria = new MemoryStream();
                    workbook.SaveAs(memoria);
                    var nombreExcel = "Reporte.xlsx";
                    var archivo = File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                    return archivo;
                }
            }
            catch (Exception)
            {
                throw;

            }
        }


    }
}
