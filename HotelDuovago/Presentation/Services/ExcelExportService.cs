using System.Windows;

using Presentation.Enums;
using Presentation.Models;

using Microsoft.Win32;

namespace Presentation.Services
{
    public class ExcelExportService
    {
        public static void Handle(List<ExcelColumn> columns, dynamic data, string worksheetName = "data")
        {
            // Se establece el contexto de la licencia
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            // Establecemos el nombre del archivo
            var fileName = string.Format("{0}.xlsx", worksheetName);

            // Generamos el package del archivo
            using (var package = new OfficeOpenXml.ExcelPackage(new System.IO.FileInfo(fileName)))
            {
                // Generar la hoja de calculo
                var worksheet = package.Workbook.Worksheets.Add(worksheetName);

                // Agregar los encabezados
                for (int i = 0; i < columns.Count(); i++)
                {
                    var column = columns[i];
                    worksheet.Cells[1, (i + 1)].Value = column.Name.ToString();

                    switch (column.Type)
                    {
                        case ColumnType.DateTime:
                            worksheet.Column(i + 1).Style.Numberformat.Format = "yyyy-mm-dd hh:MM:ss";
                            break;
                        case ColumnType.Decimal:
                            worksheet.Column(i + 1).Style.Numberformat.Format = "#,##0.00";
                            break;
                    }
                }

                // Agregamos los datos
                worksheet.Cells["A2"].LoadFromCollection(data);

                // Ajustar las columns
                for (int i = 0; i < columns.Count(); i++)
                {
                    worksheet.Column(i + 1).AutoFit();
                }

                // Se le asigna nombre al libro de trabajo
                package.Workbook.Properties.Title = worksheetName;

                // Abrir un cuadro de dialogo para elegir la ruta y guardar
                var saveFileDialog = new SaveFileDialog 
                {
                    Filter = "Archivos Excel (*.xlsx)|*.xlsx",
                    FileName = fileName
                };

                // Si se selecciona una ruta
                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        package.SaveAs(filePath);
                        MessageBox.Show(
                            string.Format("Excel file saved successfully on: \n {0}", filePath),
                            "Great",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information
                            );
                    }
                    catch (Exception ex)
                    {
                        string errorMessage = string.Format("An error ocurred while saving the file: \n {0}", ex.Message);

                        MessageBox.Show(
                            errorMessage,
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );
                    }
                }
            }
        }
    }
}
