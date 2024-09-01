using Homework_12;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

public static class JsonManager
{
    // Сохранение данных в файл JSON
    public static string ClientDataFilePath { get; } = "ClientData.json";
    public static void SaveToJson<T>(T data)
    {
        // Сохраняем обновленный список данных в файл
        string jsonData = JsonConvert.SerializeObject(data);
        File.WriteAllText(ClientDataFilePath, jsonData);
    }

    public static void AppandToJson<T>(T data)
    {
        List<T> dataList = new List<T>();

        //Пытаемся загрузить существующие данные из файла, если файл уже существует
        if (File.Exists(ClientDataFilePath))
        {
            string jsonData = File.ReadAllText(ClientDataFilePath);

            if (!string.IsNullOrEmpty(jsonData))
            {
                dataList = LoadFromJson<List<T>>();
            }

        }

        // Добавляем новый объект данных к существующему списку
        dataList.Add(data);
        string updatedJsonData = JsonConvert.SerializeObject(dataList);
        File.WriteAllText(ClientDataFilePath, updatedJsonData);
    }

    // Загрузка данных из файла JSON
    public static T LoadFromJson<T>()
    {
        if (!File.Exists(ClientDataFilePath))
        {
            using ( File.CreateText(ClientDataFilePath) ) { } ;
        }

        string json = File.ReadAllText(ClientDataFilePath);
        if (string.IsNullOrEmpty(json))
        {
            return (typeof(T) == typeof(List<Client>)) ? (T)(object)new List<Client>() : default;
        }
        //if ( string.IsNullOrEmpty(json) ) { return default; }

        // Проверяем, является ли JSON объектом или массивом
        bool isArray = json.Trim().StartsWith("[");
        if (!isArray)
        {
            // Если JSON содержит только один объект, оборачиваем его в массив
            json = $"[{json}]";
        }
        return JsonConvert.DeserializeObject<T>(json);
    }
}