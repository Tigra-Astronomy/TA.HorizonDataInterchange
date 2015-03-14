# README #

**Horizon Data Interchange** is a command line utility for importing and exporting horizon data from various planetarium software and file formats.

The initial version supports AstroPlanner and ACP, we hope to augment this over time.

Community involvement is welcome and encouraged. Please feel free to fork the repository, work on it yourself and send us pull requests. We've tried to make the code easy to extend, but if you can see a better way, have at it!

This project is covered by the [MIT License](http://opensource.org/licenses/MIT "MIT License - a very permissive free culture license"). This is a very permissive license which basically allows anyone to do anything at all with the software without obligation. Commercial use is expressly allowed. We would of course appreciate attribution and a link to this repository and/or the [Tigra Astronomy](http://tigra-astronomy.com "Software, instruments and automation systems for astronomers") web site.

### What is this repository for? ###

* Horizon data. Interchange. A quick and simple command line utility for moving horizon data between various astronomical software and file formats.
* Current version is: 0.0 pre-release; API not frozen, expect breaking changes, anything goes.

### Command Line Syntax ###

The command line requires that the user specifies exactly one importer (`-i` or `--importer`) and exactly one exporter (`-e` or `--exporter`). The remainder of the command line is specific to the importer and exporter chosen.

Supplying the `--help` option (or no options) will result in a help message being displayed.

### Usage Example ###

The initial release has only one importer, which imports from an AstroPlanner horizon file in CSV format; and an ACP exporter, which writes horizon data directly to ACP's registry. The following two commands are equivalent.


    horizon.exe --importer AstroPlanner --exporter Acp --sourcefile file.csv
    horizon.exe -iAstroPlanner -eAcp -sfile.csv

Importers and Exporters may extend the command line with their own options.

### How do I get set up? ###

* You'll need Visual Studio 2010 or later, we recommend Visual Studio 2013. The Community Edition is free and now allows the use of plug-ins so we also highly recommend that you get yourself a ReSharper license.
* The code targets .Net Framework 4.5 although we haven't used any fancy features and it should work fine with any version if you need to change it.
* Dependencies are brought in automatically as part of the build process using NuGet.
* We use MSpec (Machine.Specifications) for our unit tests and FakeItEasy as our mocking framework. We are happy to accept any testing/mocking framework if you have a strong preference, as long as we can support it on our TeamCity build server and it doesn't exclude other developers (e.g. by having an expensive license fee).
* This is a simple utility, we will use [XCOPY deployment](http://en.wikipedia.org/wiki/XCOPY_deployment "Wikipedia"). No need for an installer, just copy the compiled files and run. Because of this, everything needed to run the utility must ship with it to maintain 'XCOPY deployment'. Please don't reference anything outside the project, or users will have a hard time ensuring they have the right dependencies to use the utility. It must be possible to simply XCopy the bin directory.
* If you are new to Git, may we suggest [Atlassian's SourceTree](http://www.sourcetreeapp.com/download/ "SourceTree download") utility? It's free, works on Windows and Mac and can use both Git and Mercurial. It has both a graphical user interface and a command prompt if you prefer that, and it has its own bundled versions of Git and Mercurial, so it is absolutely everything you need.

### Contribution guidelines ###

* We try hard to work test-first and we encourage you to do the same. If you haven't done this before, it can take some getting used to. Why not use this project as an excuse to learn?
* We're very informal, but we will review all pull requests before merging them. We used to be afraid of code reviews, but we realise now that good constructive criticism is a win for everyone, so please try to be open to suggestions. We would love it if other developers would review our code, but getting people to do code reviews seems to be as hard as pulling teeth! Anyone can participate in code reviews but please keep it constructive and forget about cosmetic details. We don't care if you don't like the layout of braces (we use Whitesmith's style, deal with it). We do care if our code is confusing, violates some guideline or best practice, or is overly complicated. We are interested in ways to make the code cleaner, more loosely coupled, more SOLID and more Agile.

### Some Suggestions for Contributors ###

* Please try to write Clean Code and stick to the SOLID principles of object oriented design.
* We use ***GitFlow*** as our branching strategy, it has worked very well for us across a number of projects. The bare minimum you must know about GitFlow to get started is:
	* The `master` branch is reserved for releases. Commits to `master` are forbidden, except to merge a `release/*` branch.
	* Development happens on the `develop` branch, or preferably a feature branch off develop. Feature branches are conventionally named `feature/*`.
	* As far as possible, do one self-contained feature or bugfix per branch and then merge it back into `develop`.
	* Find out all the gory details at [http://nvie.com/posts/a-successful-git-branching-model/](http://nvie.com/posts/a-successful-git-branching-model/ "GitFlow - a successful Git branching strategy")
	* We are not a fan of rebasing. We prefer to push everything, so everyone can see what happened.
* If you commit *binaries* or *build artefacts* to the ***source code*** repository, or create any folders containing the words 'copy', 'backup', 'old' or a version number, then we will hunt you down and give you the wedgie you deserve! We've seen people do all of the above and it reveals a singular lack of understanding about what a version control system does! Please think about what you commit.


### Who do I talk to? ###

* Repo owner is Tim Long of [Tigra Astronomy](http://tigra-astronomy.com). I can be contact via my [BitBucket profile](https://bitbucket.org/tigranetworks "About Tim Long") page.



#Writing Your Own Importer or Exporter#

Importers and exporters are discovered dynamically by searching for classes that implement `IHorizonImporter` or `IHorizonExporter`. Instances are created using the parameterless constructor based on command line arguments.

So for example, if the user specifies the following command:

<pre>Horizon.exe --Importer Wibbly --Exporter Wobbly</pre>

then an attempt will be made to load `WibblyImporter` and `WobblyExporter`. Your class name should therefore be meaningful to the potential user and must end in either `Importer` or `Exporter` and must have a parameterless constructor.

Importers must implement `IHorizonImporter` and exporters must implement `IHorizonExporter`. You can implement both in the same class, but two separate instances will be created at runtime so you'll need to consider the implications of that.

At run time, your `ProcessCommandLineArguments()` method will be called, passing in the raw command line argument list as an array of strings exactly as it was passed to `Main()` by the operating system and a parser object that you can use to parse your command line options. It is your responsibility to parse the command line and act on any options that are relevant to your class; you should ignore any options you don't recognize. You can do this any way you are comfortable with, but keep in mind that you get *all* of the command line options, not just the ones relevant to you. We suggest handling command line parsing like this:

Create a data transfer object (a class with nothing but properties) to hold the parsed results of your options. For example:  

    internal sealed class MyOptions
        {
        [Option('s',"SourceFile", Required=true, HelpText = "The input file name.")]
        public string SourceFile { get; set; }
        }

Next, use the command line parser to parse the arguments into your options:

        var options = parser.ParseArguments<MyOptions>(args);
        if (options.Errors.Any())
            {
            Environment.ExitCode=-1;	// Set a -ve exit code to have help printed out on the console automatically
			throw new ArgumentException();	// Throw an appropriate exception.
            }

Once everyone has had a chance to handle their command line options, your `ImportHorizon()` or `ExportHorizon()` method will be called.

- Importers are expected to construct and return a `HorizonData` object populated with the imported data. Only add data that you've actually got, don't try to 'fill in the gaps'.
- Exporters will be handed a `HorizonData` object and can use it to create exported data at whatever resolution is needed, up to a maximum of 1 degree steps (many apps only need measurements every 5 or 10 degrees).

### The HorizonData object ###

The `HorizonData` object is implemented as a specialized derivative of `Dictionary<int, HorizonDatum>`, where the integer key is the azimuth of the reading and the value is an instance of `HorizonDatum`, which contains altitude measurements for both the solid horizon and the light dome.

Where `HorizonData` differs from an ordinary dictionary is that it is possible to read from keys that don't exist, and the values returned in the `HorizonDatum` will be interpolated values. Note that the altitude values are `double`, because they may have been interpolated. You may truncate or round these values as your needs dictate.

This automatic interpolation makes the import/export process rather straightforward.

1. To import the horizon, add each imported datum to the appropriate azimuth in your `HorizonData` collection. If your imported data has measurements every 10 degrees, then you will create entries for 0, 10, 20, 30 and so on up to 340, 350. You only need to add as many items as you obtain from the imported data set, there is no need to 'fill in the gaps'
2. To export, simply read off whatever azimuths you need to create your exported data. If your application expects a reading every 2 degrees, then simply read off values at 2 degree increments: 0, 2, 4, 6, ... 356, 358. There is no need to check whether each value exists - you will automatically get interpolated values if there were gaps in the data.



*Copyright © 2015 Tigra Astronomy, all rights reserved.*