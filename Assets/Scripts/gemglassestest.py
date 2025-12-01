from google import genai

client = genai.Client()
wltdo = client.files.upload(file="/Users/flexschool/Downloads/mixkit-dog-barking-twice-1.wav")

response = client.models.generate_content(
    model="gemini-2.5-flash",
    contents=["explain this sound in 5 words: ", wltdo],
)

print(response.text)

response = client.models.generate_content(
    model="gemini-2.5-flash",
    contents=[input("feedback?")],
)
print(response.text)