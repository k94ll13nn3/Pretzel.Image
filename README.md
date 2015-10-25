# Pretzel.Image
An image tag for Pretzel

This is a plugin for the static site generation tool [Pretzel](https://github.com/Code52/pretzel).

This plugin aims at creating an image tag that is more extensive that the markdown one.

### Usage

The syntax is the following :

`{% img path/to/file [class(es)] [width [height]] %}`

Be sure to enclose your classes in quotes if there is more than one.

### Installation

Compile the solution `Pretzel.Image.sln` and copy `Pretzel.Image.dll` to the `_plugin` folder at the root of your site folder (VS2015 needed).