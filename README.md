# SkyDesert.Localization
A lightweight, flexible and extensible localization library for .NET used in "Sky Desert" video game.
## Features
- Simple and friendly data format ("key=translation" syntax). It is way more convenient to use than common localization file types like ".json", ".yaml", ".xliff", etc, because it does not require any external localization editors other than a simple text editor.
- Detailed error handling in the parsing with Roslyn-like diagnostics.
- Recursive translation - translations can refer to another translation via "{key}" placeholders, which would be replaced with specific translations.
- Placeholders in translations can be replaced via arguments in the key, e.g., "key(placeholder=value, placeholder2=value, ...)".
- Default implementation of the locale registry to change locales on run.
- No static state - literally zero static members in the library.
- Thread safe - uses concurrent data types under the hood.
## TODO:
- Improve unescaping in the translation process.
- Refactor Translator to be extensible.
- Implement plural formatting.
- Implement translation cache.
- Implement parsing of common localization file types.
- Implement importing other localization files in the default file format.
#
**No AI was used in the creation of this project. I do not support generative AI and "vibecoding". The project is distributed under the NON-AI MIT license.**
