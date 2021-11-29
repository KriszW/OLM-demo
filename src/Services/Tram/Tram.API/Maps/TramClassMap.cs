using CsvHelper;
using CsvHelper.Configuration;
using OLM.Services.Tram.API.Models;
using OLM.Shared.Exceptions.APIResponse.Exceptions;
using OLM.Shared.Models.Tram.SharedAPIModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OLM.Services.MoneyExchangeRate.API.Maps
{
    public class TramClassMap : ClassMap<TramDataModel>
    {
        public TramClassMap()
        {
            Map(m => m.ID).Constant(null);
            Map(m => m.Date).Name("Dátum", "Date");
            Map(m => m.Shift).Convert(row =>
            {
                var text = row.Row.GetField<string>(GetColumnName(row, new string[] { "Műszak", "Shift" }));

                var parseResult = Enum.TryParse(text, out ShiftTypes shiftType);

                return parseResult ? shiftType : throw new APIErrorException($"A '{text}' nem egy valid műszak típus, használd a (De - Délelőtt) vagy a (Du - Délután) vagy a (Ej - Éjjel) típusokat.");
            });
            Map(m => m.MachineID).Name("Gép", "MachineID");
            Map(m => m.NumberOfLamella).Name("Lamellák száma", "NumberOfLamella");
            Map(m => m.NumberOfTrams).Name("Csillék száma", "NumberOfTrams");
            Map(m => m.Dimension).Convert(row => new TramDimensionModel { ID = null, Dimension = row.Row.GetField(GetColumnName(row, new string[] { "Dimension", "Dimenzió" })) });
        }

        private static string GetColumnName(ConvertFromStringArgs row, string[] columnNames)
        {
            foreach (var item in columnNames)
            {
                if (row.Row.HeaderRecord.Contains(item)) return item;
            }

            return default;
        }
    }
}
