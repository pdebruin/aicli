using Azure;
using Azure.AI.Inference;
using Azure.Identity;

class Render
{
    public async static Task PrintStream(StreamingResponse<StreamingChatCompletionsUpdate> response)
    {
        bool isThinking = false;
        await foreach (StreamingChatCompletionsUpdate chatUpdate in response)
        {
            if (!string.IsNullOrEmpty(chatUpdate.ContentUpdate))
            {
                string content = chatUpdate.ContentUpdate;
                if (content == "<think>")
                {
                    isThinking = true;
                    Console.Write("🧠 Thinking...");
                    Console.Out.Flush();
                }
                else if (content == "</think>")
                {
                    isThinking = false;
                    Console.WriteLine("🛑\n\n");
                }
                else if (!string.IsNullOrEmpty(content))
                {
                    Console.Write(content);
                    Console.Out.Flush();
                }
            }
        }
    }
}