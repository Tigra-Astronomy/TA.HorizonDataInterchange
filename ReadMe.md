# README #

**Horizon Data Interchange** is a command line utility for importing and exporting horizon data from various planetarium software and file formats.

The initial version supports AstroPlanner and ACP, we hope to augment this over time.

Community involvement is welcome and encouraged. Please feel free to fork the repository, work on it yourself and send us pull requests. We've tried to make the code easy to extend, but if you can see a better way, have at it!

This project is covered by the [MIT License](http://opensource.org/licenses/MIT "MIT License - a very permissive free culture license"). This is a very permissive license which basically allows anyone to do anything at all with the software without obligation. Commercial use is expressly allowed. We would of course appreciate attribution and a link to this repository and/or the [Tigra Astronomy](http://tigra-astronomy.com "Software, instruments and automation systems for astronomers") web site.

### What is this repository for? ###

* A quick and simple command line utility for moving horizon data between various astronomical software and file formats.
* Current version is: 0.0 pre-release; API not frozen, expect breaking changes.

### Usage Example ###
    horizon.exe -source Astroplanner -SourceFile ExportedHorizon.csv -destination ACP

    horizon.exe -source ACP -destination CsvFile -DestinationFile c:\exported.csv


### How do I get set up? ###

* You'll need Visual Studio 2010 or later, we recommend Visual Studio 2013. The Community Edition is free and now allows the use of plug-ins so we also highly recommend that you get yourself a ReSharper license.
* Dependencies are brought in automatically as part of the build process using NuGet.
* We use MSpec (Machine.Specifications) for our unit tests and FakeItEasy as our mocking framework. We are happy to accept any testing/mocking framework if you have a strong preference, as long as we can support it on our TeamCity build server and it doesn't exclude other developers (e.g. by having an expensive license fee).
* This is a simple utility, we will use [XCOPY deployment](http://en.wikipedia.org/wiki/XCOPY_deployment "Wikipedia"). No need for an installer, just copy the compiled files and run.

### Contribution guidelines ###

* We try hard to work test-first and we encourage you to do the same. If you haven't done this before, it can take some getting used to. Why not use this project as an excuse to learn?
* We're very informal, but we will review all pull requests before accepting them. We used to be afraid of code reviews, but we realise now that constructive criticism is a good thing. Anyone can participate in code reviews but please keep it constructive. We don't care if you don't like the layout of braces. We do care if an O(n-squared) algorithm is used when an O(n) algorithm would have done the job.
* Please try to write Clean Code and stick to the SOLID principles of object oriented design.

### Who do I talk to? ###

* Repo owner is Tim Long of [Tigra Astronomy](http://tigra-astronomy.com). I can be contact via my [BitBucket profile](https://bitbucket.org/tigranetworks "About Tim Long") page.


Copyright © 2015 Tigra Astronomy, all rights reserved.