# Svelto.Ioc.Example
Example for the Svelto.Ioc library https://github.com/sebas77/Svelto.IoC

This is a complete unity project. It's not nice to see, but it does the job. If you have better examples, please let me know. The Svelto.IoC library is added as submodule, therefore it will point to its own repository. You must update the submodule to get the files from the right repository.

# Note: since I wrote this project, I changed my mind about the IoC Container pattern. Nowadays I would use it only and exclusively as support tool for a GUI framework. However, without the hierarchical container feature, it wouldn't be useful for that either. I will add the hierarchical container feature in order to complete the project though. Today, I use mainly the Svelto.ECS pattern for my projects. IoC can lead to severe design issues, as I am going to write on a new article when I have time. The example is also quite old (I wrote it in 2012 and never updated it) and shows how awkward the code could get. In this case, the dependencies are mainly managers, which are way better handled by the Svelto ECS pattern.
