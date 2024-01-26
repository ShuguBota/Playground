# Introduction
The goal in the 2024 is to have some kind of code committed in each day of the year. This repository represents my attempt of learning more about certain topics that are of my interests or to add the scripts that I have created to help my productivity (definitely did not spent more time on them than it would have taken to do it manually).
# Current projects
## .NET API for Algorithmic Trading
A .NET8 API which makes use of a [yFinance](https://github.com/TimHanewich/Yahoo.Finance) library to download data and store it locally into a PostgreSQL database, if the data is not already in the local database.
It can currently download multiple stocks in a single timeframe and can also save it as a csv file which I am currently using for the python scripts, due to the fact that there are more tutorials on python and then when learning some strategies I will translate the good ones to C# code.
I try to learn through it how to structure in a clear architectural pattern the whole code.
I would like to add more features to display the data and also be able to test different strategies of trading.
## Python Scripts
In the [PythonScripts](https://github.com/ShuguBota/Algorithmic_Trading/tree/main/PythonScripts) folder I have different scripts that I made.
### [Korean simple letters learner](https://github.com/ShuguBota/Algorithmic_Trading/blob/main/PythonScripts/kr_play.py)
This script is linked with the [kr_simple_characters.json](https://github.com/ShuguBota/Algorithmic_Trading/blob/main/JSONFiles/kr_simple_characters.json) file and it aids my learning of Korean characters in a fun way.
### Image extractor
As a proof of concept I created an [image extractor](https://github.com/ShuguBota/Algorithmic_Trading/blob/main/PythonScripts/harfile_image_extractor.py) for a [.har](https://en.wikipedia.org/wiki/HAR_(file_format)) file type which then [merges](https://github.com/ShuguBota/Algorithmic_Trading/blob/main/PythonScripts/imgs_to_pdf.py) all the images into a pdf.
This was done to prove that even though there are some restrictions on downloading certain pdfs from a website, and the pdfs can only be viewed online, there are still ways to avoid the limits imposed by the pdf convertor on the website.
Even though there were some encodings of base64 on the contents of the images it was still an easy task to decode those, showing once again that security through obscurity does not work.