using Discord;
using Discord.Webhook;
using ETF_Discord_Notification_System;
using Newtonsoft.Json;

string etf_endpoint = "https://efee.etf.unibl.org:8443/api/public/oglasne-ploce/";

Configuration configuration = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText("config.json"))!;

DiscordWebhookClient discordClient = new(configuration.WebhookUrl);

HttpClient httpClient = new();

do
{
    int lastNotification = 0;
    if (File.Exists("last_notification.txt"))
    {
        lastNotification = int.Parse(File.ReadAllText("Last_notification.txt"));
    }
    int maxId = lastNotification;
    foreach (var year in configuration.Years!)
    {
        if (year.Notified)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{etf_endpoint}{year.Year}");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Root[] oglasi = Root.FromJson(jsonResponse);
            var newOglasi = oglasi?.Where(o => o.Id > lastNotification).Reverse().ToList();
            if (newOglasi!.Any())
            {
                int currentYearMax = newOglasi!.MaxBy(o => o.Id)!.Id;
                maxId = currentYearMax > maxId ? currentYearMax : maxId;
                foreach (var oglas in newOglasi!)
                {
                    EmbedBuilder builder = new()
                    {
                        Footer = new()
                        {
                            Text = $"{oglas.Potpis} {oglas.VrijemeKreiranja}",
                        },
                        Title = oglas.Naslov
                    };
                    builder.AddField(oglas.Naslov, oglas.Sadrzaj);
                    string message = year.RoleId != null && year.RoleId != 0 ? $"<@&{year.RoleId}>" : $"@everyone";
                    await discordClient.SendMessageAsync(message, embeds: [builder.Build()], threadId: year.ThreadId);
                }
            }
        }
    }
    File.WriteAllText("last_notification.txt", maxId.ToString());
    Thread.Sleep(configuration.Timeout * 60000);
} while (true);