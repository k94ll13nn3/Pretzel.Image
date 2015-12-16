# Pretzel.Image

[![Build status](https://ci.appveyor.com/api/projects/status/4oe8v8cn60bsu3af?svg=true)](https://ci.appveyor.com/project/k94ll13nn3/pretzel-image)
[![Release](https://img.shields.io/github/release/k94ll13nn3/Pretzel.Image.svg)](https://github.com/k94ll13nn3/Pretzel.Image/releases/latest)

An image tag for Pretzel

This is a plugin for the static site generation tool [Pretzel](https://github.com/Code52/pretzel).

This plugin aims at creating an image tag that is more extensive that the markdown one.

### Usage

The syntax is the following :

`{% img path/to/file [class(es)] [width [height]] %}`

Be sure to enclose your classes in quotes if there is more than one.

### Installation

Download the [latest release](https://github.com/k94ll13nn3/Pretzel.Image/releases/latest) and copy `Pretzel.Image.dll` to the `_plugins` folder at the root of your site folder.