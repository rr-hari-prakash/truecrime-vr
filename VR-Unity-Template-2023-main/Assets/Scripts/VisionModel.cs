using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

public class VisionModel : MonoBehaviour
{
    [SerializeField] private string openAIUrl = "https://api.openai.com/v1/chat/completions";
    [SerializeField] private string apiKey = "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"; 

    public List<string> imageUrls = new List<string>();
    public string queryMessage = "Describe what you see. How might it be related to a crime?";

    void Start()
    {
        // getImage();
        // StartCoroutine(PostImageQueryRequest(imageUrls, HandleResponse));
        StartCoroutine(PostTextQueryRequest("how are you", HandleResponse));
    }

    private void HandleResponse(string response)
    {
        if (response != null)
        {
            Debug.Log("Response: " + response);
        }
    }

    

   public void Tirgger(InputAction.CallbackContext callbackContext){
        
        getImage();
        StartCoroutine(PostImageQueryRequest(imageUrls, HandleResponse));
   }

    IEnumerator PostTextQueryRequest(string queryMessage, Action<string> onResponse)
    {
        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "user", content = "how are you" }
            },
            max_tokens = 300
        };

        string json = JsonUtility.ToJson(requestBody);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(openAIUrl, json, "application/json"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            // webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            // webRequest.uploadHandler.contentType = "application/json";
            // webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + apiKey);

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
                onResponse?.Invoke(null);
            }
            else
            {
                Debug.Log("Response: " + webRequest.downloadHandler.text);
                onResponse?.Invoke(webRequest.downloadHandler.text);
            }
        }
    }

    IEnumerator PostImageQueryRequest(List<string> urls, Action<string> onResponse)
    {
        var requestBody = new
        {
            model = "gpt-4-vision-preview",
            messages = BuildImageQueryMessages(urls),
            max_tokens = 300
        };

        string json = JsonUtility.ToJson(requestBody);

        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(openAIUrl, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            webRequest.uploadHandler.contentType = "application/json";
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + apiKey);

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
                onResponse?.Invoke(null);
            }
            else
            {
                Debug.Log("Response: " + webRequest.downloadHandler.text);
                onResponse?.Invoke(webRequest.downloadHandler.text);
            }
        }
    }

    private object[] BuildImageQueryMessages(List<string> urls)
    {
        var messages = new List<object>
        {
            new { type = "text", text = queryMessage }
        };

        foreach (var url in urls)
        {
            messages.Add(new { type = "image_url", image_url = url });
        }

        return messages.ToArray();
    }

    public void getImage(){
        ScreenCapture.CaptureScreenshot("screenshot.png");
        imageUrls.Add(Application.persistentDataPath + "/screenshot.png");
        Debug.Log("Screenshot Captured");
    }

  
}
