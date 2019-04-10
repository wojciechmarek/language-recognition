# LanguageRecognition
Language Recognition by Artificial Neural Network

## About
Application based on WPF technology with Material Design controls theme. Applied also Model-View-ViewModel pattern with Dependency Injection.


## How it works
One of the most easiest method of detecting language is recognition by counting the frequency of letters in text. Application consist of 3 modules: 
- Prepare text samples
- Load samples to learn and save model
- Recognize new text by trained model


## GUI
### Main Window
Main window of application. It gives access to 3 modules.
<p align="center">
  <img width="auto" height="237" alt="Main Window" src="https://user-images.githubusercontent.com/27026036/55724267-56dd6200-5a0b-11e9-93d2-4c426a817d8b.PNG">
</p>

### Prepare Window
Prepare window for preparing examples of text from different languages. Every CREATE SAMPLE press will generate new file or will append to existing file percentage values of letters. Texts in the given language should have the same label.
<p align="center">
  <img width="auto" height="489" alt="Prepare Window" src="https://user-images.githubusercontent.com/27026036/55724264-5644cb80-5a0b-11e9-9d5d-7b180cd27a50.PNG">
</p>

### Train Window
Train window allows load samples of languages and save trained model in specific location.
<p align="center">
  <img width="auto" height="340" alt="Train Window" src="https://user-images.githubusercontent.com/27026036/55724265-56dd6200-5a0b-11e9-94cd-98a30f2363f4.PNG">
</p>

### Recognize Window
Recognize window recognizes input text based on the following model of trained Artificial Neural Network.
<p align="center">
  <img width="auto" height="479" alt="Recognize Window" src="https://user-images.githubusercontent.com/27026036/55724266-56dd6200-5a0b-11e9-96d4-bdb69ffbdf98.PNG">
</p>

## Used libraries
- SharpLearning
- Castle Windsor
- Material Desing Themes
- Newtonsoft.Json
- RelayCommand
- NUnit 

## Samples
Samples of 5 languages and trained network based on that file are available in Samples directory.

## Requirements
- Windows
- .Net Framework 4.6.1
