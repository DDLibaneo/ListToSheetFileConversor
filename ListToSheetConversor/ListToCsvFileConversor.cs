using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ListToSheetConversor;

public static class ListToCsvFileConversor
{
    /// <summary>
    /// Converts a list of objects to a csv file.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath"></param>
    /// <param name="objectList"></param>
    /// <returns></returns>
    public static async Task ConvertListToCsvFile<T>(string filePath, List<T> objectList)
    {
        filePath = CreateCsvFile(filePath);

        PropertyInfo[] typePropertiesInfo = GetPropertiesInfoFromType<T>();

        var stringBuilder = new StringBuilder();

        await Task.Run(async () =>
        {
            AppendCsvHeaderToStringBuilder(typePropertiesInfo, stringBuilder);

            AppendCsvContentToStringBuilder(objectList, typePropertiesInfo, stringBuilder);

            await WriteStringBuilderContentToFile(filePath, stringBuilder);
        });
    }

    /// <summary>
    /// Writes the Header of the csv file from model type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath">Path where the file will be written to.</param>
    /// <param name="model">The properties on this object will be used as the table header.
    /// To define the order of the fields, decorate your model with the PropertyOrderAttribute.
    /// To define the name of the fields, decorate your model with the PropertyDisplayNameAttribute</param>
    /// For examples of the above see the ClientDTO.
    /// <returns></returns>
    public async static Task WriteHeaderToCsvFile<T>(string filePath, T model)
    {
        try
        {
            filePath = CreateCsvFile(filePath);

            PropertyInfo[] typePropertiesInfo = GetPropertiesInfoFromType<T>();

            var stringBuilder = new StringBuilder();

            AppendCsvHeaderToStringBuilder(typePropertiesInfo, stringBuilder);

            await WriteStringBuilderContentToFile(filePath, stringBuilder);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    /// <summary>
    /// Writes the list content to csv file (does not write any headers).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath"></param>
    /// <param name="objectList"></param>
    /// <returns></returns>
    public async static Task WriteListContentToCsvFile<T>(string filePath, List<T> objectList)
    {
        filePath = CreateCsvFile(filePath);

        PropertyInfo[] typePropertiesInfo = GetPropertiesInfoFromType<T>();

        var stringBuilder = new StringBuilder();

        await Task.Run(async () => 
        {
            AppendCsvContentToStringBuilder(objectList, typePropertiesInfo, stringBuilder);

            await WriteStringBuilderContentToFile(filePath, stringBuilder);
        });
    }

    private static PropertyInfo[] GetPropertiesInfoFromType<T>()
    {
        var typePropertiesInfo = typeof(T)
            .GetProperties();

        var propertiesHaveOrderAttribute = typePropertiesInfo.All(t => t.GetCustomAttribute<DisplayAttribute>() != null);

        if (propertiesHaveOrderAttribute)
            typePropertiesInfo = typePropertiesInfo
                .OrderBy(t => t.GetCustomAttribute<DisplayAttribute>()?.Order)
                .ToArray();
        return typePropertiesInfo;
    }

    private static string CreateCsvFile(string filePath)
    {
        if (filePath == null)
            throw new ArgumentNullException(nameof(filePath));

        if (File.Exists(filePath))
            File.Delete(filePath);

        if (!filePath.EndsWith(".csv"))
            filePath += ".csv";

        var file = File.Create(filePath);
        file.Close();

        return filePath;
    }

    private static void AppendCsvContentToStringBuilder<T>(List<T> objectList, PropertyInfo[] typePropertiesInfo, StringBuilder stringBuilder)
    {
        foreach (var obj in objectList)
        {
            stringBuilder.Append(Environment.NewLine);

            for (int i = 0; i < typePropertiesInfo.Length; i++)
            {
                stringBuilder.Append(typePropertiesInfo[i].GetValue(obj));

                if (i < typePropertiesInfo.Length - 1)
                    stringBuilder.Append(",");
            }
        }
    }

    private static void AppendCsvHeaderToStringBuilder(PropertyInfo[] typePropertiesInfo, StringBuilder stringBuilder)
    {
        for (int i = 0; i < typePropertiesInfo.Length; i++)
        {
            var displayName = typePropertiesInfo[i]
                .GetCustomAttribute<DisplayAttribute>()?.Name;

            stringBuilder.Append(displayName ?? typePropertiesInfo[i].Name);

            if (i < typePropertiesInfo.Length - 1)
                stringBuilder.Append(",");
        }
    }

    private static async Task WriteStringBuilderContentToFile(string filePath, StringBuilder stringBuilder)
    {
        using (TextWriter streamWriter = new StreamWriter(filePath, true))
            await streamWriter.WriteAsync(stringBuilder.ToString());
    }
}
