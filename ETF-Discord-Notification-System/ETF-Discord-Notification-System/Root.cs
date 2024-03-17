using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace ETF_Discord_Notification_System;

public class OglasnaPloca
{
    [JsonProperty("id")]
    public int Id;

    [JsonProperty("naziv")]
    public string? Naziv;

    [JsonProperty("opis")]
    public string? Opis;

    [JsonProperty("napomena")]
    public string? Napomena;
}

public class OglasPrilozi
{
    [JsonProperty("id")]
    public int? Id;

    [JsonProperty("naziv")]
    public string? Naziv;

    [JsonProperty("velicina")]
    public int? Velicina;

    [JsonProperty("originalniNaziv")]
    public string? OriginalniNaziv;

    [JsonProperty("ekstenzija")]
    public string? Ekstenzija;
}

public class Root
{
    [JsonProperty("id")]
    public int Id;

    [JsonProperty("naslov")]
    public string? Naslov;

    [JsonProperty("uvod")]
    public string? Uvod;

    [JsonProperty("sadrzaj")]
    public string? Sadrzaj;

    [JsonProperty("potpis")]
    public string? Potpis;

    [JsonProperty("vrijemeKreiranja")]
    public DateTime? VrijemeKreiranja;

    [JsonProperty("vrijemeIsteka")]
    public DateTime? VrijemeIsteka;

    [JsonProperty("oglasnaPloca")]
    public OglasnaPloca? OglasnaPloca;

    [JsonProperty("korisnickiNalog")]
    public object? KorisnickiNalog;

    [JsonProperty("oglasPrilozi")]
    public List<OglasPrilozi>? OglasPrilozi;

    public static Root[] FromJson(string json) => JsonConvert.DeserializeObject<Root[]>(json, JsonConverter.Settings)!;
}

internal static class JsonConverter
{
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
    };
}