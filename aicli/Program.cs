using Azure;
using Azure.AI.Inference;
using Azure.Identity;

var endpoint = "https://models.inference.ai.azure.com";
var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN"); // Your GitHub Access Token
var client = new ChatCompletionsClient(new Uri(endpoint), new AzureKeyCredential(token));

// new ChatRequestSystemMessage("You are a helpful assistant that knows about AI")
// Model = "Phi-3-medium-4k-instruct"
// var userMessage = Console.ReadLine();

var crsm = new ChatRequestSystemMessage("You are a helpful assistant.");
var crum = new ChatRequestUserMessage("How many languages are in the world?");
// var cram = new ChatRequestAssistantMessage("The capital of France is Paris");
// var crum2 = new ChatRequestUserMessage("What about Spain?");

var cco = new ChatCompletionsOptions();
// cco.Model = "Phi-3-medium-4k-instruct";
cco.Model = "Phi-4-mini-instruct";

// cco.Temperature = 1;
// cco.MaxTokens = 1000;
// cco.p

cco.Messages.Add(crsm);
cco.Messages.Add(crum);
// cco.Messages.Add(cram);
// cco.Messages.Add(crum2);

// Response<ChatCompletions> response = client.Complete(cco);

// Console.WriteLine($"Response: {response.Value.Content}");
// Console.WriteLine($"Model: {response.Value.Model}");
// Console.WriteLine("Usage:");
// Console.WriteLine($"\tPrompt tokens: {response.Value.Usage.PromptTokens}");
// Console.WriteLine($"\tTotal tokens: {response.Value.Usage.TotalTokens}");
// Console.WriteLine($"\tCompletion tokens: {response.Value.Usage.CompletionTokens}");

// //ChatResponseMessage? assistantMessage = response.Choices.First().Message;
// //Console.WriteLine($"Assistant: {assistantMessage.Content}");

StreamingResponse<StreamingChatCompletionsUpdate> streamResponse = await client.CompleteStreamingAsync(cco);

await Render.PrintStream(streamResponse);