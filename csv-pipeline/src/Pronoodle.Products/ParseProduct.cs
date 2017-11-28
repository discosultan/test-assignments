using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Globalization;
using System.IO;

namespace Pronoodle.Products
{
    /// <summary>
    /// Provides utility methods for parsing strings into <see cref="Product"/> model.
    /// </summary>
    public static class ParseProduct
    {
        static readonly Configuration CsvConfig = new Configuration
        {
            HasHeaderRecord = false,
            CultureInfo = CultureInfo.InvariantCulture
        };

        /// <summary>
        /// Attempts to parse all the entries of provided csv into products.
        /// </summary>
        /// <param name="csv">Csv to parse.</param>
        /// <param name="hasHeader">Specifies whether the csv has header row.</param>
        /// <returns>A result providing all the successes as well as errors for each csv entry.</returns>
        /// <exception cref="ArgumentNullException"><see cref="csv"/> is <c>null</c>.</exception>
        public static BatchResult<Product, string> FromCsv(string csv, bool hasHeader)
        {
            if (csv == null)
                throw new ArgumentNullException(nameof(csv));

            var result = new BatchResult<Product, string>();

            using (var reader = new StringReader(csv))
            using (var csvReader = new CsvReader(reader, CsvConfig))
            {
                // Skip header.
                if (hasHeader) reader.ReadLine();

                while (csvReader.Read())
                {
                    try
                    {
                        var product = csvReader.GetRecord<Product>();
                        result.Successes.Add(product);
                    }
                    catch (TypeConverterException ex)
                    {
                        result.Errors.Add(
                            $"'{string.Join(",", csvReader.Context.Record)}', error near text '{ex.Text}'"
                        );
                    }
                }
            }
            
            return result;
        }
    }
}
