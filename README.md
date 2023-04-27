# LanguageRecognition

This project allows to detection of a language of a provided text.

## Purpose

LanguageRecognition was made while I was in my third year of study to pass an individual project. Later I based on it my Bachelor's thesis.

## How it works

One of the easiest methods of detecting language is recognition by counting the frequency of letters in text. Each language uses letters differently, for example:

- in the Polish language is popular a letter: `w`
- in the English language is popular a letter: `h`

Here is a screenshot from an unknown source found somewhere on the Internet (Dear Author! - forgive me 🥹) showing the distribution of a letter in polish, english, and french:

![letters](https://user-images.githubusercontent.com/27026036/206008339-20b6db47-1f1f-4e28-921c-37e43b92774f.png)

Having that knowledge, we can create an Artificial Neural Network. An ANN has to have 26 input neurons in the input layer, some hidden layers, and X amount of neurons in the output layer. The X amount depends on how many languages we want to detect.

Later, after providing a new text, we have to programmatically count the amount of every letter in the text, and divide it by the total amount of letters to obtain a value in the range of 0 to 1. Next, we pass the values to input neurons and in the result, the network should also generate values in the range of 0 to 1 on output neurons. The neuron having the highest value suggests language prediction. That's all 😇.

## Screenshots

- Main window view - shows 3 modules:

  ![main](https://user-images.githubusercontent.com/27026036/55724267-56dd6200-5a0b-11e9-93d2-4c426a817d8b.PNG)

- Prepare window view - allows preparing examples from different languages. Every CREATE SAMPLE press will generate a new file or will append to existing file percentage values of letters. Texts in the given language should have the same label:

  ![prepare](https://user-images.githubusercontent.com/27026036/55724264-5644cb80-5a0b-11e9-9d5d-7b180cd27a50.PNG)

- Train window view - allows loading samples of languages and saving the trained model in a specific location:

  ![train](https://user-images.githubusercontent.com/27026036/55724265-56dd6200-5a0b-11e9-94cd-98a30f2363f4.PNG)

- Recognize window view - allows recognizing input text based on the following model of trained Artificial Neural Network:

  ![recognize](https://user-images.githubusercontent.com/27026036/55724266-56dd6200-5a0b-11e9-96d4-bdb69ffbdf98.PNG)

## Used libraries

- Material Design Themes
- Visual Studio 2019
- Newtonsoft.Json
- Castle Windsor
- .Net Framework
- SharpLearning
- MVVM Pattern
- RelayCommand
- NUnit

## Languages samples

Samples of 5 languages (🇵🇱, 🇺🇸, 🇩🇪, 🇫🇷, 🇮🇹) and trained network based on that file are available in the `/Samples` directory.

## How to run

Take a computer with Windows OS. Install .Net Framework and Visual Studio. Open the project file, and follow the instructions on what else to install, then build and run it. Lastly, load a trained model and try to detect the language of any text.
