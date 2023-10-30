import sys
import speech_recognition

file_index = sys.argv[1]
print(file_index)

wav_file = f'D:\\Прочее\\Курсач ЗАПРА\\Ershik\\Ershik\\bin\\Debug\\demo{file_index}.wav'

r = speech_recognition.Recognizer()

with speech_recognition.AudioFile(wav_file) as source:
    r.adjust_for_ambient_noise(source)
    audio = r.listen(source)

    try:
        print(str(r.recognize_google(audio,language='ru-RU')).lower())

    except Exception as e:
        print(e)