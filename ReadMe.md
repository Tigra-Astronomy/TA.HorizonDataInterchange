# README #

**Horizon Data Interchange** is a command line utility for importing and exporting horizon data from various planetarium software and file formats.

The initial version supports AstroPlanner and ACP, we hope to augment this over time.

Community involvement is welcome and encouraged. Please feel free to fork the repository, work on it yourself and send us pull requests. We've tried to make the code easy to extend, but if you can see a better way, have at it!

This project is covered by the [MIT License](http://opensource.org/licenses/MIT "MIT License - a very permissive free culture license"). This is a very permissive license which basically allows anyone to do anything at all with the software without obligation. Commercial use is expressly allowed. We would of course appreciate attribution and a link to this repository and/or the [Tigra Astronomy](http://tigra-astronomy.com "Software, instruments and automation systems for astronomers") web site.

### What is this repository for? ###

* Horizon data. Interchange. A quick and simple command line utility for moving horizon data between various astronomical software and file formats.
* Current version is: 0.0 pre-release; API not frozen, expect breaking changes, anything goes.

### Usage Example ###

The initial release has some options hard coded so that it imports from Astroplanner and exports to ACP. Only the source file needs to be specified, like so:


    horizon.exe --sourcefile data-exported-from-astroplanner.csv
    horizon.exe -s data-exported-from-astroplanner.csv

In future versions there will need to be much more flexibility on the command line. Some examples of the envisioned syntax:

    horizon.exe --from Astroplanner -SourceFile ExportedHorizon.csv --to ACP
    horizon.exe --from ACP --to CsvFile --DestinationFile c:\exported.csv


### How do I get set up? ###

* You'll need Visual Studio 2010 or later, we recommend Visual Studio 2013. The Community Edition is free and now allows the use of plug-ins so we also highly recommend that you get yourself a ReSharper license.
* Dependencies are brought in automatically as part of the build process using NuGet.
* We use MSpec (Machine.Specifications) for our unit tests and FakeItEasy as our mocking framework. We are happy to accept any testing/mocking framework if you have a strong preference, as long as we can support it on our TeamCity build server and it doesn't exclude other developers (e.g. by having an expensive license fee).
* This is a simple utility, we will use [XCOPY deployment](http://en.wikipedia.org/wiki/XCOPY_deployment "Wikipedia"). No need for an installer, just copy the compiled files and run.

### Contribution guidelines ###

* We try hard to work test-first and we encourage you to do the same. If you haven't done this before, it can take some getting used to. Why not use this project as an excuse to learn?
* We're very informal, but we will review all pull requests before accepting them. We used to be afraid of code reviews, but we realise now that constructive criticism is a good thing. Anyone can participate in code reviews but please keep it constructive. We don't care if you don't like the layout of braces. We do care if an O(n-squared) algorithm is used when an O(n) algorithm would have done the job.
* Please try to write Clean Code and stick to the SOLID principles of object oriented design.
* We use ***GitFlow*** as our branching strategy, it has worked very well for us across a number of projects. The bare minimum you must know about GitFlow to get started is:
	* The `master` branch is reserved for releases. Commits to `master` are disabled.
	* Development happens on the `develop` branch, or preferably a feature branch off develop. Feature branches are conventionally named `feature/name-of-feature`.
	* As far as possible, do one feature per branch and then merge it back into `develop`.
	* Find out all the gory details at [http://nvie.com/posts/a-successful-git-branching-model/](http://nvie.com/posts/a-successful-git-branching-model/ "GitFlow - a successful Git branching strategy")
	* We are not a fan of rebasing. Push everything, so everyone can see what happened.
* If you commit *binaries* or *build artefacts* to the *source code repository*, we will hunt you down and give you the wedgie you deserve!


### Who do I talk to? ###

* Repo owner is Tim Long of [Tigra Astronomy](http://tigra-astronomy.com). I can be contact via my [BitBucket profile](https://bitbucket.org/tigranetworks "About Tim Long") page.



#Writing Your Own Importer or Exporter#

Importers and exporters are discovered dynamically by searching for classes that implement `IHorizonImporter` or `IHorizonExporter`. Instances are created using the parameterless constructor based on command line arguments.

So for example, if the user specifies the following command:

<pre>Horizon.exe --Importer Wibbly --Exporter Wobbly</pre>

then an attempt will be made to load `WibblyImporter` and `WobblyExporter`. Your class name should therefore be meaningful to the potential user and must end in either `Importer` or `Exporter` and must have a parameterless constructor.

Importers must implement `IHorizonImporter` and exporters must implement `IHorizonExporter`.

At run time, your `ProcessCommandLineArguments()` method will be called, passing in the raw command line argument list as an array of strings. It is your responsibility to parse the command line and act on any options that are relevant to your class. You can do this any way you are comfortable with, but keep in mind that you get *all* of the command line options, not just the ones relevant to you. We suggest handling this operation like this:

Create a data transfer object (a class with nothing but properties) to hold the parsed results of your options. For example:  

    sealed class MyOptions
        {
        [Option('s',"SourceFile", Required=true, HelpText = "The CSV file name.")]
        public string SourceFile { get; set; }
        }

Next, use the command line parser to parse the arguments into your options:

        var caseInsensitiveParser = new Parser(with =>
        {
            with.CaseSensitive = false;
            with.IgnoreUnknownArguments = true;
            with.HelpWriter = Console.Error;
        });
        var options = caseInsensitiveParser.ParseArguments<MyOptions>(args);
        if (options.Errors.Any())
            {
            Environment.Exit(-1);	// Help is printed automatically
            }

It is important to use the `IgnoreUnknownArguments` setting.

Once you've parsed your command line options, your `ImportHorizon()` or `ExportHorizon()` method will be called.

- Importers are expected to construct and return a `HorizonData` object populated with the imported data. Only add data that you've actually got, don't try to 'fill in the gaps'.
- Exporters will be handed a `HorizonData` object and can use it to create exported data at whatever resolution is needed, up to a maximum of 1 degree steps (many apps only need measurements every 5 or 10 degrees).

### The HorizonData object ###

The `HorizonData` object is implemented as a specialized derivative of `Dictionary<int, HorizonDatum>`, where the integer key is the azimuth of the reading and the value is an instance of `HorizonDatum`, which contains altitude measurements for both the solid horizon and the light dome.

Where `HorizonData` differs from an ordinary dictionary is that it is possible to read from keys that don't exist, and the values returned in the `HorizonDatum` will be interpolated values. Note that the altitude values are `double`, because they may have been interpolated. You may truncate or round these values as your needs dictate.

This automatic interpolation makes the import/export process rather straightforward.

1. To import the horizon, add each imported datum to the appropriate azimuth in your `HorizonData` collection. If your imported data has measurements every 10 degrees, then you will create entries for 0, 10, 20, 30 and so on up to 340, 350. You only need to add as many items as you obtain from the imported data set, there is no need to 'fill in the gaps'
2. To export, simply read off whatever azimuths you need to create your exported data. If your application expects a reading every 2 degrees, then simply read off values at 2 degree increments: 0, 2, 4, 6, ... 356, 358. There is no need to check whether each value exists - you will automatically get interpolated values if there were gaps in the data.



*Copyright © 2015 Tigra Astronomy, all rights reserved.*