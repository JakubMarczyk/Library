using Library.API.dto;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public class MainDialog : ComponentDialog
{
    private readonly ILogger<MainDialog> _logger;

    public MainDialog(ILogger<MainDialog> logger)
        : base(nameof(MainDialog))
    {
        _logger = logger;

        // Add dialogs here

        AddDialog(new TextPrompt(nameof(TextPrompt)));
        AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
        {
                IntroStepAsync,   
                SearchBookStepAsync,
                FinalStepAsync
        }));

        InitialDialogId = nameof(WaterfallDialog);
    }

    private async Task<DialogTurnResult> IntroStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        // Use the text provided in FinalStepAsync or the default if it is the first time.
        var messageText = stepContext.Options?.ToString() ?? "W czym mogę Ci pomóc?\nNapisz mi autora, tytuł albo kategorię, a ja zobaczę co mamy";
        var promptMessage = MessageFactory.Text(messageText, messageText, InputHints.ExpectingInput);
        return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
    }
    private async Task<DialogTurnResult> SearchBookStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        var message = stepContext.Context.Activity.Text;
        var searchedText = message;

        if ( message.Length > 0)
        {
            searchedText = message.TrimStart();
        }

        var books = await CallSearchBooksApi(searchedText);

        if (books != null && books.Any())
        {
            var botMessage = "Udało mi się znaleść następujące tytuły:\n";
            foreach (var book in books)
            {
                botMessage += $"- {book.title}\n";
                var avaibleBooks = book.book_instances.Where(instance => instance.status.status_id == 1).ToList();
                if (!avaibleBooks.Any())
                    botMessage += $", niestety żaden egzamplarz nie jest aktualnie dostępny\n";
                else {
                    botMessage += $", możesz ją znaleść w:";
                    botMessage += $"\r\n";
                    foreach (var instance in avaibleBooks)
                    {
                        botMessage += $"{instance.bookshelf.name}";
                        botMessage += $"\r\n";
                    }
                }
            }
            await stepContext.Context.SendActivityAsync(MessageFactory.Text(botMessage), cancellationToken);
        }
        else
        {
            await stepContext.Context.SendActivityAsync("Niestety, nie znalazłem żadnych książek pasujących do Twojego zapytania.");
        }

        // Proceed to the next step
        return await stepContext.NextAsync(null, cancellationToken);
    }

    private async Task<List<BookDto>> CallSearchBooksApi(string searchedText)
    {
        // Adres Twojego API
        string apiUrl = "https://localhost:7299/api/Books/Search/";

        try
        {
            // Utwórz klienta HTTP
            using (HttpClient client = new HttpClient())
            {
                // Ustaw nagłówki, jeśli potrzebne
                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "your-access-token");

                // Wyślij zapytanie GET do Twojego API
                HttpResponseMessage response = await client.GetAsync(apiUrl + searchedText);

                // Sprawdź, czy odpowiedź jest sukcesem
                if (response.IsSuccessStatusCode)
                {
                    // Odczytaj zawartość odpowiedzi jako ciąg znaków
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(apiUrl + searchedText);
                    // Deserializuj JSON na listę książek
                    List<BookDto> books = JsonSerializer.Deserialize<List<BookDto>>(jsonContent);

                    return books;
                }
                else
                {
                    // Obsłuż błąd, jeśli odpowiedź nie jest sukcesem
                    Console.WriteLine($"API call failed with status code: {response.StatusCode}");
                    return null;
                }
            }
        }
        catch (Exception ex)
        {
            // Obsłuż wyjątek, jeśli wystąpił
            Console.WriteLine($"An error occurred while calling the API: {ex.Message}");
            return null;
        }
    }

    private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        var promptMessage = "Mogę pomóc w czymś jeszcze?";
        return await stepContext.ReplaceDialogAsync(InitialDialogId, promptMessage, cancellationToken);
    }
}
