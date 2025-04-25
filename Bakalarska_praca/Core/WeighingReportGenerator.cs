using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System.Diagnostics;
using Bakalarska_praca.Core.Models;

public static class WeighingReportGenerator
{
    public static void GenerateReport(WeighingDisplayModel weighing)
    {
        var doc = new Document();
        var section = doc.AddSection();

        var title = section.AddParagraph($"Vážny lístok č. {weighing.Id}");
        title.Format.Font.Size = 14;
        title.Format.Alignment = ParagraphAlignment.Center;
        title.Format.SpaceAfter = "1cm";

        var table = section.AddTable();
        table.Borders.Width = 0.75;

        table.AddColumn("5cm");
        table.AddColumn("10cm");

        void AddRow(string label, string value, bool bold = false)
        {
            var row = table.AddRow();
            row.Cells[0].AddParagraph(label);
            var valuePara = row.Cells[1].AddParagraph(value);
            if (bold) valuePara.Format.Font.Bold = true;
        }

        AddRow("ID váženia:", weighing.Id.ToString());
        AddRow("Typ:", weighing.Type);
        AddRow("Dátum váženia:", weighing.DateTime.ToString("dd.MM.yyyy HH:mm"));
        AddRow("Príchod (Brutto čas):", weighing.BruttoTime.ToString("HH:mm"));

        // Partner, vozidlo a materiál
        AddRow("Partner:", weighing.Partner);
        AddRow("Vozidlo:", weighing.Truck);
        AddRow("Materiál:", weighing.Material);

        // Hmotnosti
        AddRow("Brutto:", $"{weighing.Brutto} kg");
        AddRow("Tara:", $"{weighing.Tara} kg");
        AddRow("Netto:", $"{weighing.Netto} kg");

        // Ďalšie info
        AddRow("Poznámka:", string.IsNullOrWhiteSpace(weighing.Note) ? "-" : weighing.Note);
        AddRow("Vystavil:", weighing.User);

        // Render & save
        var renderer = new PdfDocumentRenderer(true) { Document = doc };
        renderer.RenderDocument();

        string fileName = $"VaznyListok_{weighing.Id}.pdf";
        renderer.PdfDocument.Save(fileName);

        Process.Start("explorer.exe", fileName);
    }
}
