IoTGateway
======================

**IoTGateway** is a C# implementation of an IoT gateway. It is self-contained, and includes all libraries and frameworks 
it needs to operate. You can install it by using the following [IoT Gateway Setup application](Executables/IoTGatewaySetup.exe?raw=true).
Example applications also include binary downloads.

Apart from the [IoT Gateway](#iot-gateway) projects, the solution is divided into different groups of projects and modules:

* [Clients](#clients)
* [Content](#content)
* [Events](#events)
* [Layout](#layout)
* [Mocks](#mocks)
* [Networking](#networking)
* [Persistence](#persistence)
* [Runtime](#runtime)
* [Script](#script)
* [Security](#security)
* [Services](#services)
* [Themes](#themes)
* [Things](#things)
* [Utilities](#utilities)
* [Web Services](#webServices)

License
----------------------

You should carefully read the following terms and conditions before using this software. Your use of this software indicates
your acceptance of this license agreement and warranty. If you do not agree with the terms of this license, or if the terms of this
license contradict with your local laws, you must remove any files from the **IoT Gateway** from your storage devices and cease to use it. 
The terms of this license are subjects of changes in future versions of the **IoT Gateway**.

You may not use, copy, emulate, clone, rent, lease, sell, modify, decompile, disassemble, otherwise reverse engineer, or transfer the
licensed program, or any subset of the licensed program, except as provided for in this agreement.  Any such unauthorised use shall
result in immediate and automatic termination of this license and may result in criminal and/or civil prosecution.

The [source code](https://github.com/PeterWaher/IoTGateway) and libraries provided in this repository is provided open for the following uses:

* For **Personal evaluation**. Personal evaluation means evaluating the code, its libraries and underlying technologies, including learning 
	about underlying technologies.

* For **Academic use**. If you want to use the following code for academic use, all you need to do is to inform the author of who you are, 
	what academic institution you work for (or study for), and in what projects you intend to use the code. All that is asked in return is for 
	an acknowledgement and visible attribution to this repository, including a link, and that you do not redistribute the source code, or parts 
	thereof in the solutions you develop. If any solutions developed in an academic setting, become commercial, it will need a commercial license.

* For **Security analysis**. If you perform any security analysis on the code, to see what security aspects the code might have, all that is 
	asked of you, is that you inform the author of any findings at least forty-five days before publication of the findings, so that any vulnerabilities 
	might be addressed. Such contributions are much appreciated and will be acknowledged.

Commercial use of the code, in part or in full, in compiled binary form, or its source code, requires
a **Commercial License**. Contact the author for details.

**Note**: Distribution of code in source or compiled form, for purposes other than mentioned
above, is not considered personal use and requires a commercial license, even if distribution 
is made under an apparently free license. It facilitates the development of competing 
software, without the investment in actually performing the corresponding coding. It also 
can make the use of the original libraries obsolete, as free code apparently doing the same, 
based on the original libraries, would be available under an apparently free license. (Thus, 
making distribution free does not mitigate this effect.) Developers using the libraries to 
enhance their own projects (brands, offerings or businesses, even if the software itself is 
free), should therefore consider sponsoring the development of such software. It is the 
express intent of the developer of these libraries to create libraries that facilitate the 
development of great software for IoT. Also, the commercial license includes options to 
request customizations of the libraries.

All rights to the source code are reserved and exclusively owned by [Waher Data AB](http://waher.se/). 
Any contributions made to the **IoT Gateway** repository become the intellectual property of [Waher Data AB](http://waher.se/).
If you're interested in using the source code, as a whole, or in part, you need a license agreement 
with the author. You can contact him through [LinkedIn](http://waher.se/).

This software is provided by the copyright holder and contributors "as is" and any express or implied warranties, including, but not limited to, 
the implied warranties of merchantability and fitness for a particular purpose are disclaimed. In no event shall the copyright owner or contributors 
be liable for any direct, indirect, incidental, special, exemplary, or consequential damages (including, but not limited to, procurement of substitute 
goods or services; loss of use, data, or profits; or business interruption) however caused and on any theory of liability, whether in contract, strict 
liability, or tort (including negligence or otherwise) arising in any way out of the use of this software, even if advised of the possibility of such 
damage.

The **IoT Gateway** is &copy; [Waher Data AB](http://waher.se/) 2016-2021. All rights reserved.
 
[![](/Images/logo-WaherDataAB-300x58.png)](http://waher.se/)

Mastering Internet of Things
-----------------------------------

Many of the libraries available in this repository contains are described and explained
in the book *Mastering Internet of Things* by Peter Waher. You can find the book
on [Packt](https://www.packtpub.com/networking-and-servers/mastering-internet-things),
[Amazon](https://www.amazon.com/Mastering-Internet-Things-Peter-Waher/dp/1788397487/),
[Bokus](https://www.bokus.com/bok/9781788397483/mastering-internet-of-things/)
and other stores.

![Mastering Internet of Things Book Cover](/Images/Cover.png)

The examples described in this book are available in a separate repository:
[MIoT](https://github.com/PeterWaher/MIoT)

IoT Gateway
----------------------

The IoT Gateway is represented by the following set of projects. They are back-end server applications and perform 
communiction with devices, as well as host online content.
You can install it by using the following [IoT Gateway Setup application](Executables/IoTGatewaySetup.exe?raw=true).

| Project                         | Type          | Link                                                                                   | Project description |
|---------------------------------|---------------|----------------------------------------------------------------------------------------|---------------------|
| **Waher.IoTClient.Setup**       | Wix           | [Installer](Executables/IoTClientSetup.exe?raw=true)                                   | The [Waher.IoTClient.Setup](Waher.IoTClient.Setup) project creates a Windows setup application that bootstraps several bundles into one setup application. Apart from installing the IoT Client, it also installs any prerequisites, such as the correct .NET framework. It is based on in [Wix framework](https://www.firegiant.com/wix/). |
| **Waher.IoTClient.Win32**       | Wix           | [Installer](Executables/Waher.IoTClient.Win32.msi?raw=true)                            | The [Waher.IoTClient.Win32](Waher.IoTClient.Win32) project creates a Windows MSI package that installs the Windows 32-bit files for the IoT Client. Project is based on in [Wix framework](https://www.firegiant.com/wix/). |
| **Waher.IoTGateway**            | .NET Std 2.0  | [NuGet](https://www.nuget.org/packages/Waher.IoTGateway/)                              | The [Waher.IoTGateway](Waher.IoTGateway) project is a class library that defines the IoT Gateway. The gateway can host any web content. It converts markdown to HTML in real-time. It can be administrated over XMPP using the [Waher.Client.WPF](Clients/Waher.Client.WPF) application. |
| **Waher.IoTGateway.App**        | UWP           | [App Bundle Package](Executables/Waher.IoTGateway.App_x86_x64_arm.appxbundle?raw=true) | The [Waher.IoTGateway.App](Waher.IoTGateway.App) project is a Universal Windows Platform application version of the IoT Gateway. It can be installed on IoT devices running Windows 10 IoT. |
| **Waher.IoTGateway.Build**      | .NET Core 3.1 |                                                                                        | The [Waher.IoTGateway.Build](Waher.IoTGateway.Build) project contains MSBuild script for building setup files. Can be used in an auto-build environment. |
| **Waher.IoTGateway.Console**    | .NET Core 3.1 | [Installer](Executables/IoTGatewaySetup.exe?raw=true)                                  | The [Waher.IoTGateway.Console](Waher.IoTGateway.Console) project is a console application version of the IoT Gateway. It's easy to use and experiment with. |
| **Waher.IoTGateway.Installers** | .NET 4.6.2    |                                                                                        | The [Waher.IoTGateway.Installers](Waher.IoTGateway.Installers) project defines custom actions used by the setup application to install the IoT Gateway and dependencies propertly. |
| **Waher.IoTGateway.Resources**  | .NET Std 1.0  |                                                                                        | The [Waher.IoTGateway.Resources](Waher.IoTGateway.Resources) project contains resource files that are common to all IoT Gateway embodiments. |
| **Waher.IoTGateway.Setup**      | Wix           | [Installer](Executables/IoTGatewaySetup.exe?raw=true)                                  | The [Waher.IoTGateway.Setup](Waher.IoTGateway.Setup) project creates a Windows setup application that bootstraps several bundles into one setup application. Apart from installing the IoT Gateway, it also installs any prerequisites, such as the correct .NET framework. It is based on in [Wix framework](https://www.firegiant.com/wix/). |
| **Waher.IoTGateway.Svc**        | .NET Core 3.1 | [Installer](Executables/IoTGatewaySetup.exe?raw=true)                                  | The [Waher.IoTGateway.Svc](Waher.IoTGateway.Svc) project is a Windows Service version version of the IoT Gateway. |
| **Waher.IoTGateway.Win32**      | Wix           | [Installer](Executables/Waher.IoTGateway.Win32.msi?raw=true)                           | The [Waher.IoTGateway.Win32](Waher.IoTGateway.Win32) project creates a Windows MSI package that installs the Windows 32-bit files for the IoT Gateway. Project is based on in [Wix framework](https://www.firegiant.com/wix/). |

Clients
----------------------

The [Clients](Clients) folder contains projects starting with **Waher.Client.** and denote client projects. Clients are front-end applications that 
can be run by users to perform different types of interaction with things or the network.

| Project                          | Type       | Link                                                            | Project description |
|----------------------------------|------------|-----------------------------------------------------------------|---------------------|
| **Waher.Client.WPF**             | .NET 4.6.2 | [Installer](Executables/IoTClientSetup.exe?raw=true)            | The [Waher.Client.WPF](Clients/Waher.Client.WPF) project is a simple IoT client that allows you to interact with things and users. If you connect to the network, you can chat with users and things. The client GUI is built using Windows Presentation Foundation (WPF). Chat sessions support normal plain text content, and rich content based on markdown. |
| **Waher.Client.MqttEventViewer** | .NET 4.6.2 | [Executable](Executables/Waher.Client.MqttEventViewer?raw=true) | The [Waher.Client.MqttEventViewer](Clients/Client.MqttEventViewer) project defines a simple WPF client application that subscribes to an MQTT topic and displays any events it receivs. Events are parsed as XML fragments, according to the schema defined in [XEP-0337](http://xmpp.org/extensions/xep-0337.html). |

Content
----------------------

The [Content](Content) folder contains libraries that handle Internet Content including parsing and rendering, using their
corresponding Internet Content Type encodings and decodings.

| Project                                  | Type         | Link                                                                          | Project description |
|------------------------------------------|--------------|-------------------------------------------------------------------------------|---------------------|
| **Waher.Content**                        | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Content/)                        | The [Waher.Content](Content/Waher.Content) project is a class library that provides basic abstraction for Internet Content Type, and basic encodings and decodings. This includes handling and parsing of common data types. |
| **Waher.Content.Asn1**                   | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Content.Asn1/)                   | The [Waher.Content.Asn1](Content/Waher.Content.Asn1) project implements a simple ASN.1 (Abstract Syntax Notation One) parser. The library supports generation of C# code from ASN.1 schemas. Encoding/Decoding schemes supported: BER, CER, DER. |
| **Waher.Content.Dsn**                    | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Content.Dsn/)                    | The [Waher.Content.Dsn](Content/Waher.Content.Dsn) project provides encoding and decoding of Delivery Status Notification (DSN) messages and message reports, as defined in RFC 3462 and 3464. |
| **Waher.Content.Emoji**                  | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Content.Emoji/)                  | The [Waher.Content.Emoji](Content/Waher.Content.Emoji) project contains utilities for working with emojis. |
| **Waher.Content.Emoji.Emoji1**           | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Content.Emoji.Emoji1/)           | The [Waher.Content.Emoji.Emoji1](Content/Waher.Content.Emoji.Emoji1) project provide free emojis from [Emoji One](http://emojione.com/) to content applications. |
| **Waher.Content.Html**                   | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Content.Html/)                   | The [Waher.Content.Html](Content/Waher.Content.Html) project provides a simple HTML document parser that can be used to extract information from web pages. Social Meta-data can be easily extracted from page. Information is taken from Open Graph meta data or Twitter Card meta data, as well as standard HTML meta data. |
| **Waher.Content.Images**                 | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Content.Images/)                 | The [Waher.Content.Images](Content/Waher.Content.Images) project contains encoders and decoders for images. It uses [SkiaSharp](https://www.nuget.org/packages/SkiaSharp) for cross-platform 2D graphics manipulation. Contains extraction of EXIF meta-data from images. |
| **Waher.Content.Markdown**               | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Content.Markdown/)               | The [Waher.Content.Markdown](Content/Waher.Content.Markdown) project can be used to parse Markdown documents and transforms them to other formats, such as HTML, Plain text and XAML. For a description of the markdown flavour supported by the parser, see [Markdown documentation](https://waher.se/Markdown.md). The library can also compare Markdown documents, and provide Markdown-based difference documents, showing how one version of a document is edited to produce a second version. |
| **Waher.Content.Markdown.Consolidation** | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Content.Markdown.Consolidation/) | The [Waher.Content.Markdown.Consolidation](Content/Waher.Content.Markdown.Consolidation) project helps clients working with Markdown defined with the Markdown engine in [Waher.Content.Markdown](Content/Waher.Content.Markdown) to consolidate Markdown content originating from multiple sources, generating composite documents for more intuitive presentation. |
| **Waher.Content.Markdown.GraphViz**      | .NET Std 2.0 | [NuGet](https://www.nuget.org/packages/Waher.Content.Markdown.GraphViz/)      | The [Waher.Content.Markdown.GraphViz](Content/Waher.Content.Markdown.GraphViz) project extends the capabilities of the Markdown engine defined in [Waher.Content.Markdown](Content/Waher.Content.Markdown). It allows for real-time inclusion and generation of [GraphViz](http://graphviz.org/) diagrams, if the software is installed on the system. [Markdown documentation](https://waher.se/Markdown.md#graphvizDiagrams). |
| **Waher.Content.Markdown.Layout2D**      | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Content.Markdown.Layout2D/)      | The [Waher.Content.Markdown.Layout2D](Content/Waher.Content.Markdown.Layout2D) project extends the capabilities of the Markdown engine defined in [Waher.Content.Markdown](Content/Waher.Content.Markdown). It allows for real-time inclusion and generation of [Waher.Layout.Layout2D](Layout/Waher.Layout.Layout2D) diagrams. [Markdown documentation](https://waher.se/Markdown.md#2dLayoutDiagrams). |
| **Waher.Content.Markdown.PlantUml**      | .NET Std 2.0 | [NuGet](https://www.nuget.org/packages/Waher.Content.Markdown.PlantUml/)      | The [Waher.Content.Markdown.PlantUml](Content/Waher.Content.Markdown.PlantUml) project extends the capabilities of the Markdown engine defined in [Waher.Content.Markdown](Content/Waher.Content.Markdown). It allows for real-time inclusion and generation of [PlantUML](https://plantuml.com/) diagrams, if the software is installed on the system. [Markdown documentation](https://waher.se/Markdown.md#umlWithPlantuml). |
| **Waher.Content.Markdown.SystemFiles**   | .NET Std 2.0 | [NuGet](https://www.nuget.org/packages/Waher.Content.Markdown.SystemFiles/)   | The [Waher.Content.Markdown.SystemFiles](Content/Waher.Content.Markdown.SystemFiles) project helps modules to find files and applications installed on the system, for integration purposes. |
| **Waher.Content.Markdown.Web**           | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Content.Markdown.Web/)           | The [Waher.Content.Markdown.Web](Content/Waher.Content.Markdown.Web) project allows the publishing of web content using Markdown. The library converts Markdown documents in real-time to HTML when hosted using the web server defined in [Waher.Networking.HTTP](Content/Waher.Networking.HTTP). |
| **Waher.Content.Markdown.Web.UWP**       | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Content.Markdown.Web.UWP/)       | The [Waher.Content.Markdown.Web.UWP](Content/Waher.Content.Markdown.Web.UWP) project allows the publishing of web content using Markdown. The library converts Markdown documents in real-time to HTML when hosted using the web server defined in [Waher.Networking.HTTP.UWP](Content/Waher.Networking.HTTP.UWP). |
| **Waher.Content.QR**                     | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Content.QR/)                     | The [Waher.Content.QR](Content/Waher.Content.QR) contains a light-weight managed encoder of QR codes. It can generate both text-based output (using block characters) for display on text devices, as well as images and color-coded codes. |
| **Waher.Content.SystemFiles**            | .NET Std 2.0 | [NuGet](https://www.nuget.org/packages/Waher.Content.SystemFiles/)            | The [Waher.Content.SystemFiles](Content/Waher.Content.SystemFiles) helps modules to find files and applications installed on the system, for integration purposes. |
| **Waher.Content.Xml**                    | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Content.Xml/)                    | The [Waher.Content.Xml](Content/Waher.Content.Xml) project helps with encoding and decoding of XML. It integrates with the architecture defined in [Waher.Content](Content/Waher.Content). |
| **Waher.Content.Xsl**                    | .NET Std 2.0 | [NuGet](https://www.nuget.org/packages/Waher.Content.Xsl/)                    | The [Waher.Content.Xsl](Content/Waher.Content.Xsl) project helps with validating and transforming XML documents. It integrates with the architecture defined in [Waher.Content](Content/Waher.Content). |

The folder also contains the following unit test projects:

| Project                            | Type          | Project description |
|------------------------------------|---------------|---------------------|
| **Waher.Content.Asn1.Test**        | .NET Core 3.1 | The [Waher.Content.Asn1.Test](Content/Waher.Content.Asn1.Test) project contains unit tests for the **Waher.Content.Asn1** project. |
| **Waher.Content.Html.Test**        | .NET Core 3.1 | The [Waher.Content.Html.Test](Content/Waher.Content.Html.Test) project contains unit tests for the **Waher.Content.Html** project. |
| **Waher.Content.Images.Test**      | .NET Core 3.1 | The [Waher.Content.Images.Test](Content/Waher.Content.Images.Test) project contains unit tests for the **Waher.Content.Images** project. |
| **Waher.Content.Markdown.Test**    | .NET Core 3.1 | The [Waher.Content.Markdown.Test](Content/Waher.Content.Markdown.Test) project contains unit tests for the **Waher.Content.Markdown** project. |
| **Waher.Content.QR.Test**          | .NET Core 3.1 | The [Waher.Content.QR.Test](Content/Waher.Content.QR.Test) project contains unit tests for the **Waher.Content.QR** project. |
| **Waher.Content.Test**             | .NET Core 3.1 | The [Waher.Content.Test](Content/Waher.Content.Test) project contains unit tests for the **Waher.Content** project. |


Events
----------------------

The [Events](Events) folder contains libraries that manage different aspects of event logging in networks.

| Project                          | Type         | Link                                                                  | Project description |
|----------------------------------|--------------|-----------------------------------------------------------------------|---------------------|
| **Waher.Events**                 | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Events/)                 | The [Waher.Events](Events/Waher.Events) project provides the basic architecture and framework for event logging in applications. It uses the static class **Log** as a hub for all type of event logging in applications. To this hub you can register any number of **Event Sinks** that receive events and distribute them according to implementation details in each one. By logging all events to **Log** you have a configurable environment where you can change logging according to specific needs of the project. |
| **Waher.Events.Console**         | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Events.Console/)         | The [Waher.Events.Console](Events/Waher.Events.Console) project provides a simple event sink, that outputs events to the console standard output. Useful, if creating simple console applications. |
| **Waher.Events.Documentation**   | XML          |                                                                       | The [Waher.Events.Documentation](Events/Waher.Events.Documentation) project contains documentation of specific important events. This documentation includes Event IDs and any parameters they are supposed to include. |
| **Waher.Events.Files**           | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Events.Files/)           | The [Waher.Events.Files](Events/Waher.Events.Files) project defines event sinks that outputs events to files. Supported formats are plain text and XML. XML files can be transformed using XSLT to other formats, such as HTML. |
| **Waher.Events.MQTT**            | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Events.MQTT/)            | The [Waher.Events.MQTT](Events/Waher.Events.MQTT) project defines an event sink that sends events to an MQTT topic. Events are sent as XML fragments, according to the schema defined in [XEP-0337](http://xmpp.org/extensions/xep-0337.html). |
| **Waher.Events.MQTT.UWP**        | UWP          | [NuGet](https://www.nuget.org/packages/Waher.Events.MQTT.UWP/)        | The [Waher.Events.MQTT.UWP](Events/Waher.Events.MQTT.UWP) project provides a Universal Windows Platform compatible version of the **Waher.Events.MQTT** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Events.Persistence**     | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Events.Persistence/)     | The [Waher.Events.Persistence](Events/Waher.Events.Persistence) project creates an even sink that stores incoming (logged) events in the local object database, as defined by [Waher.Persistence](Persistence/Waher.Persistence). Event life time in the database is defined in the constructor. Searches can be made for historical events. |
| **Waher.Events.Statistics**      | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Events.Statistics/)      | The [Waher.Events.Statistics](Events/Waher.Events.Statistics) project defines an event sink that computes statistics of events being logged. |
| **Waher.Events.WindowsEventLog** | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Events.WindowsEventLog/) | The [Waher.Events.WindowsEventLog](Events/Waher.Events.WindowsEventLog) project defines an event sink that logs events to a Windows Event Log. |
| **Waher.Events.XMPP**            | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Events.XMPP/)            | The [Waher.Events.XMPP](Events/Waher.Events.XMPP) project defines an event sink that distributes events over XMPP, according to [XEP-0337](http://xmpp.org/extensions/xep-0337.html). |
| **Waher.Events.XMPP.UWP**        | UWP          | [NuGet](https://www.nuget.org/packages/Waher.Events.XMPP.UWP/)        | The [Waher.Events.XMPP.UWP](Events/Waher.Events.XMPP.UWP) project provides a reduced Universal Windows Platform compatible version of the **Waher.Events.XMPP** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |

Layout
----------------------

The [Layout](Layout) folder contains libraries for laying out objects visually.

| Project                            | Type         | Link                                                                    | Project description |
|------------------------------------|--------------|-------------------------------------------------------------------------|---------------------|
| **Waher.Layout.Layout2D**          | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Layout.Layout2D/)          | The [Waher.Layout.Layout2D](Layout/Waher.Layout.Layout2D) project provides an object model for laying out graphical objects in two dimensions. These models can then be used to generate images. The models can be represented in XML, and contains an XML schema that can be used to validate documents, as well as provide support when editing layout documents. |

The folder also contains the following unit test projects:

| Project                            | Type          | Project description |
|------------------------------------|---------------|---------------------|
| **Waher.Layout.Layout2D.Test**     | .NET Core 3.1 | The [Waher.Layout.Layout2D.Test](Layout/Waher.Layout.Layout2D.Test) project contains unit tests for the **Waher.Layout.Layout2D** project. |

Mocks
----------------------

The [Mocks](Mocks) folder contains projects that implement different mock devices. These can be used as development tools to test technologies, 
implementation, networks and tools.

| Project                        | Type         | Link                                                    | Project description |
|--------------------------------|--------------|---------------------------------------------------------|---------------------|
| **Waher.Mock**                 | .NET Std 2.0 | [NuGet](https://www.nuget.org/packages/Waher.Mock/)     | The [Waher.Mock](Mocks/Waher.Mock) project is a class library that provides support for simple mock applications. This includes simple network configuration. |
| **Waher.Mock.Lamp**            | .NET 4.6.2   |                                                         | The [Waher.Mock.Lamp](Mocks/Waher.Mock.Lamp) project simulates a simple lamp switch with an XMPP interface. |
| **Waher.Mock.Lamp.UWP**        | UWP          |                                                         | The [Waher.Mock.Lamp.UWP](Mocks/Waher.Mock.Lamp.UWP) project provides a Universal Windows Platform compatible version of the **Waher.Mock.Lamp** mock application. This application can be run on Windows 10, including on Rasperry Pi. |
| **Waher.Mock.Temperature**     | .NET 4.6.2   |                                                         | The [Waher.Mock.Temperature](Mocks/Waher.Mock.Temperature) project simulates a simple temperature sensor with an XMPP interface. |
| **Waher.Mock.Temperature.UWP** | UWP          |                                                         | The [Waher.Mock.Temperature.UWP](Mocks/Waher.Mock.Temperature.UWP) project provides a Universal Windows Platform compatible version of the **Waher.Mock.Temperature** mock application. This application can be run on Windows 10, including on Rasperry Pi. |
| **Waher.Mock.UWP**             | UWP          | [NuGet](https://www.nuget.org/packages/Waher.Mock.UWP/) | The [Waher.Mock.UWP](Mocks/Waher.Mock.UWP) project provides a reduced Universal Windows Platform compatible version of the **Waher.Mock** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. It is limited in that it does not provide a console dialog for editing connection parameters if none exist. It does not include schema validation of XML configuration files either. |

Networking
----------------------

The [Networking](Networking) folder contains libraries that manage different aspects of network communication.

| Project                                        | Type          | Link                                                                               | Project description |
|------------------------------------------------|---------------|------------------------------------------------------------------------------------|---------------------|
| **Waher.Networking**                           | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking/)                          | The [Waher.Networking](Networking/Waher.Networking) project provides the basic architecture and tools for all networking libraries.  This includes sniffers, etc. |
| **Waher.Networking.Cluster**                   | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.Cluster/)                  | The [Waher.Networking.Cluster](Networking/Waher.Networking.Cluster) project provides a framework for building applications that can cooperate and solve problems in clusters. Communication between endpoints in clusters is performed using AES-256 encrypted datagrams over a predefined UDP Multicast channel. Only participants with access to the shared key can participate in the cluster. Supports Unacknowledged, Acknowledged and Assured Message transfers in clusters, as well as Request/Response command executions, Locking of singleton resources, serialization of objects, etc. |
| **Waher.Networking.Cluster.ConsoleSandbox**    | .NET Core 3.1 |                                                                                    | The [Waher.Networking.Cluster.ConsoleSandbox](Networking/Waher.Networking.Cluster.ConsoleSandbox) project provides a simple console application that allows you to interactively test the cluster protocol in the network. |
| **Waher.Networking.CoAP**                      | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.CoAP/)                     | The [Waher.Networking.CoAP](Networking/Waher.Networking.CoAP) project provides a simple CoAP endpoint client with DTLS support. |
| **Waher.Networking.CoAP.UWP**                  | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.CoAP.UWP/)                 | The [Waher.Networking.CoAP.UWP](Networking/Waher.Networking.CoAP.UWP) project provides a Universal Windows Platform compatible version of the **Waher.Networking.CoAP** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.DNS**                       | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.DNS/)                      | The [Waher.Networking.DNS](Networking/Waher.Networking.DNS) project provides a class library for resolving DNS host, mailbox and service names on the network. It also supports reverse address lookups, International Domain Names (IDN), DNS Black Lists (DNSBL), text records, and maintains a local Resource Record cache. |
| **Waher.Networking.HTTP**                      | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.HTTP/)                     | The [Waher.Networking.HTTP](Networking/Waher.Networking.HTTP) project provides a simple HTTP server for publishing dynamic content and managing user authentication based on a customizable set of users and privileges. Supports the WebSocket protocol. |
| **Waher.Networking.HTTP.UWP**                  | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.HTTP.UWP/)                 | The [Waher.Networking.HTTP.UWP](Networking/Waher.Networking.HTTP.UWP) project provides a Universal Windows Platform compatible version of the **Waher.Networking.HTTP** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.LWM2M**                     | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.LWM2M/)                    | The [Waher.Networking.LWM2M](Networking/Waher.Networking.LWM2M) project provides LWM2M interfaces for your application, using the CoAP library defined in [Waher.Networking.CoAP](Networking/Waher.Networking.CoAP). |
| **Waher.Networking.LWM2M.UWP**                 | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.LWM2M.UWP/)                | The [Waher.Networking.LWM2M.UWP](Networking/Waher.Networking.LWM2M.UWP) project provides a Universal Windows Platform compatible version of the **Waher.Networking.LWM2M** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.MQTT**                      | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.MQTT/)                     | The [Waher.Networking.MQTT](Networking/Waher.Networking.MQTT) project provides a simple MQTT client. |
| **Waher.Networking.MQTT.UWP**                  | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.MQTT.UWP/)                 | The [Waher.Networking.MQTT.UWP](Networking/Waher.Networking.MQTT.UWP) project provides a Universal Windows Platform compatible version of the **Waher.Networking.MQTT** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.PeerToPeer**                | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.PeerToPeer/)               | The [Waher.Networking.PeerToPeer](Networking/Waher.Networking.PeerToPeer) project provides tools for peer-to-peer and multi-player communication. |
| **Waher.Networking.UPnP**                      | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.UPnP/)                     | The [Waher.Networking.UPnP](Networking/Waher.Networking.UPnP) project provides tools for searching and interacting with devices in the local area network using the UPnP protocol. |
| **Waher.Networking.WHOIS**                     | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.WHOIS/)                    | The [Waher.Networking.WHOIS](Networking/Waher.Networking.WHOIS) project implements a [WHOIS](https://tools.ietf.org/html/rfc3912) client that can be used to query Regional Internet Registries for information relating to IP addresses, etc. |
| **Waher.Networking.WHOIS.UWP**                 | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.WHOIS.UWP/)                | The [Waher.Networking.WHOIS.UWP](Networking/Waher.Networking.WHOIS.UWP) project provides a Universal Windows Platform compatible version of the **Waher.Networking.WHOIS** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.XMPP**                      | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP/)                     | The [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP) project provides a simple XMPP client. |
| **Waher.Networking.XMPP.Avatar**               | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.Avatar/)              | The [Waher.Networking.XMPP.Avatar](Networking/Waher.Networking.XMPP.Avatar) project provides an add-on to the XMPP client defined in [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP). This add-on helps the client manage avatars. |
| **Waher.Networking.XMPP.Avatar.UWP**           | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.Avatar.UWP/)          | The [Waher.Networking.XMPP.Avatar.UWP](Networking/Waher.Networking.XMPP.Avatar.UWP) project provides a reduced Universal Windows Platform compatible version of the **Waher.Networking.XMPP.Avatar** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.XMPP.BOSH**                 | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.BOSH/)                | The [Waher.Networking.XMPP.BOSH](Networking/Waher.Networking.XMPP.BOSH) project provides support for the HTTP altenative binding based on BOSH (defined in [XEP-0124](http://xmpp.org/extensions/xep-0124.html) and [XEP-0206](http://xmpp.org/extensions/xep-0206.html)) to the XMPP client defined in [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP). |
| **Waher.Networking.XMPP.BOSH.UWP**             | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.BOSH.UWP/)            | The [Waher.Networking.XMPP.BOSH.UWP](Networking/Waher.Networking.XMPP.BOSH.UWP) project provides a reduced Universal Windows Platform compatible version of the **Waher.Networking.XMPP.BOSH** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.XMPP.Chat**                 | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.Chat/)                | The [Waher.Networking.XMPP.Chat](Networking/Waher.Networking.XMPP.Chat) project provides a simple XMPP chat server bot for things, that is added to the XMPP client defined in [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP). It supports markdown, and follows the chat semantics outlined in this proto-XEP: [Chat Interface for Internet of Things Devices](http://htmlpreview.github.io/?https://github.com/joachimlindborg/XMPP-IoT/blob/master/xep-0000-IoT-Chat.html) |
| **Waher.Networking.XMPP.Chat.UWP**             | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.Chat.UWP/)            | The [Waher.Networking.XMPP.Chat.UWP](Networking/Waher.Networking.XMPP.Chat.UWP) project provides a reduced Universal Windows Platform compatible version of the **Waher.Networking.XMPP.Chat** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.XMPP.Concentrator**         | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.Concentrator/)        | The [Waher.Networking.XMPP.Concentrator](Networking/Waher.Networking.XMPP.Concentrator) project provides an add-on to the XMPP client defined in [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP). This add-on provides client and server concentrator capabilities, as defined in [XEP-0326](http://xmpp.org/extensions/xep-0326.html). The concentrator interface allows a device to manage a set of internal virtual devices, all sharing the same XMPP connection. |
| **Waher.Networking.XMPP.Concentrator.UWP**     | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.Concentrator.UWP/)    | The [Waher.Networking.XMPP.Concentrator.UWP](Networking/Waher.Networking.XMPP.Concentrator.UWP) project provides a reduced Universal Windows Platform compatible version of the **Waher.Networking.XMPP.Concentrator** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.XMPP.Contracts**            | .NET Std 2.0  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.Contracts/)           | The [Waher.Networking.XMPP.Contracts](Networking/Waher.Networking.XMPP.Contracts) project provides an add-on to the XMPP client defined in [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP). This add-on provides client interfaces for managing legal identities, smart contracts and signatures, as defined in the [IEEE XMPP IoT extensions](https://gitlab.com/IEEE-SA/XMPPI/IoT). |
| **Waher.Networking.XMPP.Control**              | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.Control/)             | The [Waher.Networking.XMPP.Control](Networking/Waher.Networking.XMPP.Control) project provides an add-on to the XMPP client defined in [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP). This add-on provides client and server control capabilities, as defined in the [IEEE XMPP IoT extensions](https://gitlab.com/IEEE-SA/XMPPI/IoT). |
| **Waher.Networking.XMPP.Control.UWP**          | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.Control.UWP/)         | The [Waher.Networking.XMPP.Control.UWP](Networking/Waher.Networking.XMPP.Control.UWP) project provides a reduced Universal Windows Platform compatible version of the **Waher.Networking.XMPP.Control** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.XMPP.HTTPX**                | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.HTTPX/)               | The [Waher.Networking.XMPP.HTTPX](Networking/Waher.Networking.XMPP.HTTPX) project provides an add-on to the XMPP client defined in [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP). This add-on provides client and server HTTPX support, as defined in [XEP-0332](http://xmpp.org/extensions/xep-0332.html). It also provides an HTTP proxy for tunneling HTTPX content through an HTTP(S)-based web server hosted by [Waher.Networking.HTTP](Networking/Waher.Networking.HTTP). |
| **Waher.Networking.XMPP.Interoperability**     | .NET Std 1.3  |                                                                                    | The [Waher.Networking.XMPP.Interoperability](Networking/Waher.Networking.XMPP.Interoperability) project provides an add-on to the XMPP client defined in [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP). This add-on provides client and server interoperability capabilities, as defined in this proto-XEP: [Internet of Things - Interoperability](http://htmlpreview.github.io/?https://github.com/joachimlindborg/XMPP-IoT/blob/master/xep-0000-IoT-Interoperability.html) |
| **Waher.Networking.XMPP.Interoperability.UWP** | UWP           |                                                                                    | The [Waher.Networking.XMPP.Interoperability.UWP](Networking/Waher.Networking.XMPP.Interoperability.UWP) project provides a reduced Universal Windows Platform compatible version of the **Waher.Networking.XMPP.Interoperability** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.XMPP.Mail**                 | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.Mail/)                | The [Waher.Networking.XMPP.Mail](https://github.com/PeterWaher/IoTGateway/tree/master/Networking/Waher.Networking.XMPP.Mail) project provides an add-on to the XMPP client defined in [Waher.Networking.XMPP](https://github.com/PeterWaher/IoTGateway/tree/master/Networking/Waher.Networking.XMPP). This add-on provides client support for mail extensions on XMPP servers. |
| **Waher.Networking.XMPP.MUC**                  | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.MUC/)                 | The [Waher.Networking.XMPP.MUC](Networking/Waher.Networking.XMPP.MUC) project adds support for the Multi-User-Chat (MUC) extension (XEP-0045) to the XMPP Client library defined in [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP). Direct invitations (XEP-0249), and Self-Ping (XEP-410) are also supported. |
| **Waher.Networking.XMPP.MUC.UWP**              | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.MUC.UWP/)             | The [Waher.Networking.XMPP.MUC.UWP](Networking/Waher.Networking.XMPP.MUC.UWP) project provides a reduced Universal Windows Platform compatible version of the **Waher.Networking.XMPP.MUC** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.XMPP.P2P**                  | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.P2P/)                 | The [Waher.Networking.XMPP.P2P](Networking/Waher.Networking.XMPP.P2P) project provides classes that help the application do servless XMPP (peer-to-peer) communication, as defined in [XEP-0174](http://xmpp.org/extensions/xep-0174.html). |
| **Waher.Networking.XMPP.PEP**                  | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.PEP/)                 | The [Waher.Networking.XMPP.PEP](Networking/Waher.Networking.XMPP.PEP) project adds support for the Personal Eventing Protocol extension (XEP-0163) to the XMPP Client library defined in [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP). |
| **Waher.Networking.XMPP.PEP.UWP**              | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.PEP.UWP/)             | The [Waher.Networking.XMPP.PEP.UWP](Networking/Waher.Networking.XMPP.PEP.UWP) project provides a reduced Universal Windows Platform compatible version of the **Waher.Networking.XMPP.PEP** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.XMPP.Provisioning**         | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.Provisioning/)        | The [Waher.Networking.XMPP.Provisioning](Networking/Waher.Networking.XMPP.Provisioning) project provides an add-on to the XMPP client defined in [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP). This add-on provides client support for provisioning and delegation of trust, as defined in the [IEEE XMPP IoT extensions](https://gitlab.com/IEEE-SA/XMPPI/IoT). |
| **Waher.Networking.XMPP.Provisioning.UWP**     | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.Provisioning.UWP/)    | The [Waher.Networking.XMPP.Provisioning.UWP](Networking/Waher.Networking.XMPP.Provisioning.UWP) project provides a reduced Universal Windows Platform compatible version of the **Waher.Networking.XMPP.Provisioning** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.XMPP.PubSub**               | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.PubSub/)              | The [Waher.Networking.XMPP.PubSub](Networking/Waher.Networking.XMPP.PubSub) project adds support for the Publish/Subscribe extension (XEP-0060) to the XMPP Client library defined in [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP). |
| **Waher.Networking.XMPP.PubSub.UWP**           | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.PubSub.UWP/)          | The [Waher.Networking.XMPP.PubSub.UWP](Networking/Waher.Networking.XMPP.PubSub.UWP) project provides a reduced Universal Windows Platform compatible version of the **Waher.Networking.XMPP.PubSub** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.XMPP.RDP**                  | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.RDP/)                 | The [Waher.Networking.XMPP.RDP](Networking/Waher.Networking.XMPP.RDP) project provides an add-on to the XMPP client defined in [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP). This add-on provides client-side support for Remote Desktop over XMPP. |
| **Waher.Networking.XMPP.Sensor**               | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.Sensor/)              | The [Waher.Networking.XMPP.Sensor](Networking/Waher.Networking.XMPP.Sensor) project provides an add-on to the XMPP client defined in [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP). This add-on provides client and server sensor capabilities, as defined in the [IEEE XMPP IoT extensions](https://gitlab.com/IEEE-SA/XMPPI/IoT). |
| **Waher.Networking.XMPP.Sensor.UWP**           | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.Sensor.UWP/)          | The [Waher.Networking.XMPP.Sensor.UWP](Networking/Waher.Networking.XMPP.Sensor.UWP) project provides a reduced Universal Windows Platform compatible version of the **Waher.Networking.XMPP.Sensor** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.XMPP.Software**             | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.Software/)            | The [Waher.Networking.XMPP.Software](Networking/Waher.Networking.XMPP.Software) project provides a client for managing and downloading software packages and software updates, as defined in the [IEEE XMPP IoT extensions](https://gitlab.com/IEEE-SA/XMPPI/IoT). |
| **Waher.Networking.XMPP.Synchronization**      | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.Synchronization/)     | The [Waher.Networking.XMPP.Synchronization](Networking/Waher.Networking.XMPP.Synchronization) project provides an add-on to the XMPP client defined in [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP). This add-on provides clock synchronization capabilities, as defined in the [IEEE XMPP IoT extensions](https://gitlab.com/IEEE-SA/XMPPI/IoT). |
| **Waher.Networking.XMPP.Synchronization.UWP**  | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.Synchronization.UWP/) | The [Waher.Networking.XMPP.Synchronization.UWP](Networking/Waher.Networking.XMPP.Synchronization.UWP) project provides a reduced Universal Windows Platform compatible version of the **Waher.Networking.XMPP.Synchronization** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.XMPP.UWP**                  | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.UWP/)                 | The [Waher.Networking.XMPP.UWP](Networking/Waher.Networking.XMPP.UWP) project provides a reduced Universal Windows Platform compatible version of the **Waher.Networking.XMPP** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Networking.XMPP.WebSocket**            | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.WebSocket/)           | The [Waher.Networking.XMPP.WebSocket](Networking/Waher.Networking.XMPP.WebSocket) project provides support for the websocket altenative binding based on BOSH (defined in [RFC-7395](https://tools.ietf.org/html/rfc7395)) to the XMPP client defined in [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP). |
| **Waher.Networking.XMPP.WebSocket.UWP**        | UWP           | [NuGet](https://www.nuget.org/packages/Waher.Networking.XMPP.WebSocket.UWP/)       | The [Waher.Networking.XMPP.WebSocket.UWP](Networking/Waher.Networking.XMPP.WebSocket.UWP) project provides a reduced Universal Windows Platform compatible version of the **Waher.Networking.XMPP.WebSocket** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |

The folder also contains the following unit test projects:

| Project                            | Type          | Project description |
|------------------------------------|---------------|---------------------|
| **Waher.Networking.Cluster.Test**  | .NET Core 3.1 | The [Waher.Networking.Cluster.Test](Networking/Waher.Networking.Cluster.Test) project contains unit-tests for the [Waher.Networking.Cluster](Networking/Waher.Networking.Cluster) library. |
| **Waher.Networking.CoAP.Test**     | .NET Core 3.1 | The [Waher.Networking.CoAP.Test](Networking/Waher.Networking.CoAP.Test) project contains unit-tests for the [Waher.Networking.CoAP](Networking/Waher.Networking.CoAP) library. |
| **Waher.Networking.DNS.Test**      | .NET Core 3.1 | The [Waher.Networking.DNS.Test](Networking/Waher.Networking.DNS.Test) project contains unit-tests for the [Waher.Networking.DNS](Networking/Waher.Networking.DNS) library. |
| **Waher.Networking.HTTP.Test**     | .NET Core 3.1 | The [Waher.Networking.HTTP.Test](Networking/Waher.Networking.HTTP.Test) project contains unit-tests for the [Waher.Networking.HTTP](Networking/Waher.Networking.HTTP) library. |
| **Waher.Networking.MQTT.Test**     | .NET Core 3.1 | The [Waher.Networking.MQTT.Test](Networking/Waher.Networking.MQTT.Test) project contains unit-tests for the [Waher.Networking.MQTT](Networking/Waher.Networking.MQTT) library. |
| **Waher.Networking.WHOIS.Test**    | .NET Code 3.1 | The [Waher.Networking.WHOIS.Test](Networking/Waher.Networking.WHOIS.Test) project contains unit-tests for the [Waher.Networking.WHOIS](Networking/Waher.Networking.WHOIS) library. |
| **Waher.Networking.XMPP.Test**     | .NET Core 3.1 | The [Waher.Networking.XMPP.Test](Networking/Waher.Networking.XMPP.Test) project contains unit-tests for the [Waher.Networking.XMPP](Networking/Waher.Networking.XMPP) library and add-ons. |

Persistence
----------------------

The [Persistence](Persistence) folder contains libraries that create an infrastructure for persistence of objects in applications. 
This includes a simple embedded encrypted local object database, as well as integration with external databases. Objects are persisted based on 
their annotated class definitions.

| Project                                      | Type         | Link                                                                              | Project description |
|----------------------------------------------|--------------|-----------------------------------------------------------------------------------|---------------------|
| **Waher.Persistence**                        | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Persistence/)                        | The [Waher.Persistence](Persistence/Waher.Persistence) project provides the central interfaces for interaction with object databases. All modules can use the static **Database** class to persist and find objects in the preconfigured object database. |
| **Waher.Persistence.Files**                  | .NET Std 1.5 | [NuGet](https://www.nuget.org/packages/Waher.Persistence.Files/)                  | The [Waher.Persistence.Files](Persistence/Waher.Persistence.Files) project defines a library that provides an object database that stores objects in local AES-256 encrypted files. Storage, indices, searching and retrieval is based solely on meta-data provided through the corresponding class definitions. Object serializers are created dynamically. Dynamic code is compiled. Access is provided through the [Waher.Persistence](Persistence/Waher.Persistence) library. |
| **Waher.Persistence.FilesLW**                | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Persistence.FilesLW/)                | The [Waher.Persistence.FilesLW](Persistence/Waher.Persistence.FilesLW) project defines a library that provides an object database that stores objects in local files. Storage, indices, searching and retrieval is based solely on meta-data provided through the corresponding class definitions. Object serializers are created dynamically. Access is provided through the [Waher.Persistence](Persistence/Waher.Persistence) library. |
| **Waher.Persistence.MongoDB**                | .NET Std 1.5 | [NuGet](https://www.nuget.org/packages/Waher.Persistence.MongoDB/)                | The [Waher.Persistence.MongoDB](Persistence/Waher.Persistence.MongoDB) project provides a [MongoDB](https://www.mongodb.org/) database provider that can be used for object persistence through the [Waher.Persistence](Persistence/Waher.Persistence) library. |
| **Waher.Persistence.Serialization**          | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Persistence.Serialization/)          | The [Waher.Persistence.Serialization](Persistence/Waher.Persistence.Serialization) project defines a library that serializes objects to binary form using meta-data provided through the corresponding class definitions. Object serializers are created dynamically. Compatible with Waher.Persistence.Serialization.Compiled. |
| **Waher.Persistence.Serialization.Compiled** | .NET Std 1.5 | [NuGet](https://www.nuget.org/packages/Waher.Persistence.Serialization.Compiled/) | The [Waher.Persistence.Serialization.Compiled](Persistence/Waher.Persistence.Serialization.Compiled) project defines a library that serializes objects to binary form using meta-data provided through the corresponding class definitions. Object serializers are created dynamically. Dynamic code is compiled. Compatible with Waher.Persistence.Serialization. |

The folder also contains the following unit test projects:

| Project                            | Type          | Project description |
|------------------------------------|---------------|---------------------|
| **Waher.Persistence.Files.Test**   | .NET Core 3.1 | The [Waher.Persistence.Files.Test](Persistence/Waher.Persistence.Files.Test) project contains unit tests for the [Waher.Persistence.Files](Persistence/Waher.Persistence.Files) project. |
| **Waher.Persistence.FilesLW.Test** | .NET Core 3.1 | The [Waher.Persistence.FilesLW.Test](Persistence/Waher.Persistence.FilesLW.Test) project contains unit tests for the [Waher.Persistence.FilesLW](Persistence/Waher.Persistence.FilesLW) project. |
| **Waher.Persistence.MongoDB.Test** | .NET Core 3.1 | The [Waher.Persistence.MongoDB.Test](Persistence/Waher.Persistence.MongoDB.Test) project contains unit tests for the [Waher.Persistence.MongoDB](Persistence/Waher.Persistence.MongoDB) project. |

Runtime
----------------------

The [Runtime](Runtime) folder contains libraries that help applications with common runtime tasks, such as caching, maintaining a type inventory, 
language localization, runtime settings, timing and scheduling.

| Project                               | Type          | Link                                                                       | Project description |
|---------------------------------------|---------------|----------------------------------------------------------------------------|---------------------|
| **Waher.Runtime.Cache**               | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Runtime.Cache/)               | The [Waher.Runtime.Cache](Runtime/Waher.Runtime.Cache) project provides tools for in-memory caching. |
| **Waher.Runtime.Inventory**           | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Runtime.Inventory/)           | The [Waher.Runtime.Inventory](Runtime/Waher.Runtime.Inventory) project keeps an inventory of types and interfaces available in your code. It also provides a means to access available types given an interface, and can find the best implementation to process a task or item. It can be used to implement an Inversion of Control Pattern, and helps instantiate interfaces, abstract classes and normal classes, including recursively instantiating constructor arguments. Handles singleton types. |
| **Waher.Runtime.Inventory.Loader**    | .NET Std 2.0  | [NuGet](https://www.nuget.org/packages/Waher.Runtime.Inventory.Loader/)    | The [Waher.Runtime.Inventory.Loader](Runtime/Waher.Runtime.Inventory.Loader) project dynamically loads modules from a folder, and initiates the inventory defined in [Waher.Runtime.Inventory](Runtime/Waher.Runtime.Inventory) with all loaded and referenced assemblies. |
| **Waher.Runtime.Language**            | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Runtime.Language/)            | The [Waher.Runtime.Language](Runtime/Waher.Runtime.Language) project helps applications with language localization. |
| **Waher.Runtime.Profiling**           | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Runtime.Profiling/)           | The [Waher.Runtime.Profiling](Runtime/Waher.Runtime.Profiling) project contains tools for profiling sequences of actions in multiple threads. Results are accumulated, and can be exported to XML or as PlantUML Diagrams. |
| **Waher.Runtime.Queue**               | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Runtime.Queue/)               | The [Waher.Runtime.Queue](Runtime/Waher.Runtime.Queue) project contains a specialised FIFO Queue for asynchronous transport of items between tasks. You can have multiple working tasks adding items to the queue, as well as multiple working tasks subscribing to items from the queue. |
| **Waher.Runtime.ServiceRegistration** | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Runtime.ServiceRegistration/) | The [Waher.Runtime.ServiceRegistration](Runtime/Waher.Runtime.ServiceRegistration) library allows applications to register themselves with an XMPP-based Service Registry, such as the [IoT Broker](https://waher.se/Broker.md). |
| **Waher.Runtime.Settings**            | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Runtime.Settings/)            | The [Waher.Runtime.Settings](Runtime/Waher.Runtime.Settings) project helps applications maintain a set of persistent settings. |
| **Waher.Runtime.Text**                | .NET Std 1.0  | [NuGet](https://www.nuget.org/packages/Waher.Runtime.Text/)                | The [Waher.Runtime.Text](Runtime/Waher.Runtime.Text) project provides classes working with text and text documents, particularly find differences between texts, or sequences of symbols. |
| **Waher.Runtime.Temporary**           | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Runtime.Temporary/)           | The [Waher.Runtime.Temporary](Runtime/Waher.Runtime.Temporary) project contains classes simplifying working with temporary in-memory and file streams. |
| **Waher.Runtime.Threading**           | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Runtime.Threading/)           | The [Waher.Runtime.Threading](Runtime/Waher.Runtime.Threading) project provides classes for usage in multi-threaded asynchronous environments providing multiple-read/single-write capabilities. |
| **Waher.Runtime.Timing**              | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Runtime.Timing/)              | The [Waher.Runtime.Timing](Runtime/Waher.Runtime.Timing) project provides tools for timing and scheduling. |
| **Waher.Runtime.Transactions**        | .NET Std 1.3  | [NuGet](https://www.nuget.org/packages/Waher.Runtime.Transactions/)        | The [Waher.Runtime.Transactions](Runtime/Waher.Runtime.Transactions) project defines an architecture for processing transactions to help protect the integrity of data in asynchronous or distributed environments. |

The folder also contains the following unit test projects:

| Project                            | Type          | Project description |
|------------------------------------|---------------|---------------------|
| **Waher.Runtime.Inventory.Test**   | .NET Core 3.1 | The [Waher.Runtime.Inventory.Test](Runtime/Waher.Runtime.Inventory.Test) project contains unit tests for the [Waher.Runtime.Inventory](Runtime/Waher.Runtime.Inventory) project. |
| **Waher.Runtime.Language.Test**    | .NET Core 3.1 | The [Waher.Runtime.Language.Test](Runtime/Waher.Runtime.Language.Test) project contains unit tests for the [Waher.Runtime.Language](Runtime/Waher.Runtime.Language) project. |
| **Waher.Runtime.Profiling.Test**   | .NET Core 3.1 | The [Waher.Runtime.Profiling.Test](Runtime/Waher.Runtime.Profiling.Test) project contains unit tests for the [Waher.Runtime.Profiling](Runtime/Waher.Runtime.Profiling) project. |
| **Waher.Runtime.Settings.Test**    | .NET Core 3.1 | The [Waher.Runtime.Settings.Test](Runtime/Waher.Runtime.Settings.Test) project contains unit tests for the [Waher.Runtime.Settings](Runtime/Waher.Runtime.Settings) project. |
| **Waher.Runtime.Text.Test**        | .NET Core 3.1 | The [Waher.Runtime.Text.Test](Runtime/Waher.Runtime.Text.Test) project contains unit tests for the [Waher.Runtime.Text](Runtime/Waher.Runtime.Text) project. |
| **Waher.Runtime.Threading.Test**   | .NET Core 3.1 | The [Waher.Runtime.Threading.Test](Runtime/Waher.Runtime.Threading.Test) project contains unit tests for the [Waher.Runtime.Threading](Runtime/Waher.Runtime.Threading) project. |

Script
----------------------

The [Script](Script) folder contains libraries that define an extensible execution envionment for script supporting canonical extensions, .NET integration, 
graphs, physical units and unit conversions, etc. For more information about the script engine supported by these libraries, see the 
[script reference](https://waher.se/Script.md).


| Project                       | Type         | Link                                                               | Project description |
|-------------------------------|--------------|--------------------------------------------------------------------|---------------------|
| **Waher.Script**              | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Script/)              | The [Waher.Script](Script/Waher.Script) project is a class library that provides basic abstraction and execution model for symbolic math and scripting. It also manages pluggable modules and easy dynamic access to runtime namespaces and types. |
| **Waher.Script.Content**      | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Script.Content/)      | The [Waher.Script.Content](Script/Waher.Script.Content) project is a class library that adds content functions to the script engine, suitable for loading, fetching or processing content from files or online resources. |
| **Waher.Script.Cryptography** | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Script.Cryptography/) | The [Waher.Script.Cryptography](Script/Waher.Script.Cryptography) project is a class library that adds cryptography functions to the script engine. |
| **Waher.Script.Fractals**     | .NET Std 2.0 | [NuGet](https://www.nuget.org/packages/Waher.Script.Fractals/)     | The [Waher.Script.Fractals](Script/Waher.Script.Fractals) project is a class library that adds fractal image functions to the script engine, suitable for generating backgound images. It uses [SkiaSharp](https://www.nuget.org/packages/SkiaSharp) for cross-platform 2D graphics manipulation. |
| **Waher.Script.Graphs**       | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Script.Graphs/)       | The [Waher.Script.Graphs](Script/Waher.Script.Graphs) project is a class library that adds graphing functions to the script engine. It uses [SkiaSharp](https://www.nuget.org/packages/SkiaSharp) for cross-platform 2D graphics manipulation. |
| **Waher.Script.Graphs3D**     | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Script.Graphs3D/)     | The [Waher.Script.Graphs3D](Script/Waher.Script.Graphs3D) project is a class library that adds 3D-graphing functions to the script engine. It uses [SkiaSharp](https://www.nuget.org/packages/SkiaSharp) for cross-platform 2D graphics manipulation. |
| **Waher.Script.Lab**          | .NET 4.6.2   | [Executable (x86)](Executables/Waher.Script.Lab.x86.zip?raw=true)  | The [Waher.Script.Lab](Script/Waher.Script.Lab) project is a WPF application that allows you to experiment and work with script. |
| **Waher.Script.Networking**   | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Script.Networking/)   | The [Waher.Script.Networking](Script/Waher.Script.Networking) project is a class library that extends the script engine with functions for different network protocols. |
| **Waher.Script.Persistence**  | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Script.Persistence/)  | The [Waher.Script.Persistence](Script/Waher.Script.Persistence) project is a class library that allows access to the object database defined in the [Waher.Persistence](Persistence/Waher.Persistence) library in script. |
| **Waher.Script.Statisics**    | .NET Std 1.5 | [NuGet](https://www.nuget.org/packages/Waher.Script.Statistics/)   | The [Waher.Script.Statisics](Script/Waher.Script.Statisics) project is a class library that adds statistical functions to the script engine. |
| **Waher.Script.Xml**          | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Script.Xml/)          | The [Waher.Script.Xml](Script/Waher.Script.Xml) project is a class library that contains script extensions for parsing XML. |
| **Waher.Script.XmlDSig**      | .NET Std 2.0 | [NuGet](https://www.nuget.org/packages/Waher.Script.XmlDSig/)      | The [Waher.Script.XmlDSig](Script/Waher.Script.XmlDSig) project is a class library that contains script extensions for signing and verifying XML documents using the [XMLDSIG](https://www.w3.org/TR/xmldsig-core/) standard. |

The folder also contains the following unit test projects:

| Project                            | Type          | Project description |
|------------------------------------|---------------|---------------------|
| **Waher.Script.Test**              | .NET Core 3.1 | The [Waher.Script.Test](Script/Waher.Script.Test) project contains unit tests for the script-related projects in this section. |

Security
----------------------

The [Security](Security) folder contains libraries that are dedicated at solving particular security or data protection such as authentication, 
authorization and encryption.

| Project                             | Type         | Link                                                                     | Project description |
|-------------------------------------|--------------|--------------------------------------------------------------------------|---------------------|
| **Waher.Security**                  | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Security/)                  | The [Waher.Security](Security/Waher.Security) project provides a basic security model based on users, roles and privileges. It's not based on operating system features, to allow code to be platform independent. |
| **Waher.Security.ACME**             | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Security.ACME/)             | The [Waher.Security.ACME](Security/Waher.Security.ACME) project contains a class library implementing the ACME v2 protocol for the generation of certificates using ACME-compliant certificate servers, as defined in the [ACME draft](https://tools.ietf.org/html/draft-ietf-acme-acme-13). |
| **Waher.Security.CallStack**        | .NET Std 2.0 | [NuGet](https://www.nuget.org/packages/Waher.Security.CallStack/)        | The [Waher.Security.CallStack](Security/Waher.Security.CallStack) project provide tools for securing access to methods and properties in code, by limiting access to them to a given set of callers. This prevents unintentional leaks of information through code running in the same process. |
| **Waher.Security.ChaChaPoly**       | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Security.ChaChaPoly/)       | The [Waher.Security.ChaChaPoly](Security/Waher.Security.ChaChaPoly) project implements the ChaCha20, Poly1305 and AEAD_CHACHA20_POLY1305 algorithms, as defined in [RFC 8439](https://tools.ietf.org/html/rfc8439). |
| **Waher.Security.DTLS**             | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Security.DTLS/)             | The [Waher.Security.DTLS](Security/Waher.Security.DTLS) project contains a class library implementing the Datagram Transport Layer Security (DTLS) Version 1.2, as defined in [RFC 6347](https://tools.ietf.org/html/rfc6347). |
| **Waher.Security.EllipticCurves**   | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Security.EllipticCurves/)   | The [Waher.Security.EllipticCurves](Security/Waher.Security.EllipticCurves) project contains a class library implementing algorithms for Elliptic Curve Cryptography, such as ECDH, ECDSA, EdDSA, NIST P-192, NIST P-224, NIST P-256, NIST P-384, NIST P-521, Curve25519, Curve448, Edwards25519 and Edwards448. |
| **Waher.Security.JWS**              | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Security.JWS/)              | The [Waher.Security.JWS](Security/Waher.Security.JWS) project implements a framework for JSON Web Signatures (JWS), as defined in [RFC 7515](https://tools.ietf.org/html/rfc7515). |
| **Waher.Security.JWT**              | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Security.JWT/)              | The [Waher.Security.JWT](Security/Waher.Security.JWT) project helps applications with the creation and validation of Java Web Tokens (JWT), as defined in [RFC 7519](https://tools.ietf.org/html/rfc7519). |
| **Waher.Security.JWT.UWP**          | UWP          | [NuGet](https://www.nuget.org/packages/Waher.Security.JWT.UWP/)          | The [Waher.Security.JWT.UWP](Security/Waher.Security.JWT.UWP) project provides a Universal Windows Platform compatible version of the **Waher.Security.JWT** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Security.LoginMonitor**     | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Security.LoginMonitor/)     | The [Waher.Security.LoginMonitor](Security/Waher.Security.LoginMonitor) helps applications monitor login activity, and help block malicious entities from the system. |
| **Waher.Security.LoginMonitor.UWP** | UWP          | [NuGet](https://www.nuget.org/packages/Waher.Security.LoginMonitor.UWP/) | The [Waher.Security.LoginMonitor.UWP](Security/Waher.Security.LoginMonitor.UWP) project provides a Universal Windows Platform compatible version of the **Waher.Security.LoginMonitor** Library. This library can be used to develop applications for Windows 10, on for instance Rasperry Pi. |
| **Waher.Security.PKCS**             | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Security.PKCS/)             | The [Waher.Security.PKCS](Security/Waher.Security.PKCS) project contains classes and tools for working with Public Key Cryptography Standards (PKCS). |
| **Waher.Security.SHA3**             | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Security.SHA3/)             | The [Waher.Security.SHA3](Security/Waher.Security.SHA3) project implements SHA-3, as defined in [NIST FIPS 202](https://nvlpubs.nist.gov/nistpubs/FIPS/NIST.FIPS.202.pdf), including the general KECCAK algorithm and the SHAKE128, SHAKE256, RawSHAKE128 and RawSHAKE256 XOF functions. |
| **Waher.Security.SPF**              | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Security.SPF/)              | The [Waher.Security.SPF](Security/Waher.Security.SPF) project contains a class library for resolving Sender Policy Framework (SPF) strings as defined in [RFC 7208](https://tools.ietf.org/html/rfc7208). |
| **Waher.Security.Users**            | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Security.Users/)            | The [Waher.Security.Users](Security/Waher.Security.Users) project defines an architecture of persistent users, roles and privileges that can be used to provide detailed authorization in applications. Privileges are ordered in a tree structure. Roles contains a list of allowed privileges (nodes or entire branches), or explicitly prohibited privileges (nodes or branches). Each user can be assigned one or more roles. Credentials are protected using hash digests. Objects are persisted through the object database abstraction layer, defined in Waher.Persistence. |

The folder also contains the following unit test projects:

| Project                                | Type          | Project description |
|----------------------------------------|---------------|---------------------|
| **Waher.Security.ACME.Test**           | .NET Core 3.1 | The [Waher.Security.ACME.Test](Security/Waher.Security.ACME.Test) project contains unit tests for the  [Waher.Security.ACME](Security/Waher.Security.ACME) project.                                         |
| **Waher.Security.ChaChaPoly.Test**     | .NET Core 3.1 | The [Waher.Security.ChaChaPoly.Test](Security/Waher.Security.ChaChaPoly.Test) project contains unit tests for the  [Waher.Security.ChaChaPoly](Security/Waher.Security.ChaChaPoly) project. |
| **Waher.Security.DTLS.Test**           | .NET Core 3.1 | The [Waher.Security.DTLS.Test](Security/Waher.Security.DTLS.Test) project contains unit tests for the  [Waher.Security.DTLS](Security/Waher.Security.DTLS) project.                                         |
| **Waher.Security.EllipticCurves.Test** | .NET Core 3.1 | The [Waher.Security.EllipticCurves.Test](Security/Waher.Security.EllipticCurves.Test) project contains unit tests for the  [Waher.Security.EllipticCurves](Security/Waher.Security.EllipticCurves) project. |
| **Waher.Security.JWT.Test**            | .NET Core 3.1 | The [Waher.Security.JWT.Test](Security/Waher.Security.JWT.Test) project contains unit tests for the  [Waher.Security.JWT](Security/Waher.Security.JWT) project.                                             |
| **Waher.Security.LoginMonitor.Test**   | .NET Core 3.1 | The [Waher.Security.LoginMonitor.Test](Security/Waher.Security.LoginMonitor.Test) project contains unit tests for the  [Waher.Security.LoginMonitor](Security/Waher.Security.LoginMonitor) project. |
| **Waher.Security.PKCS.Test**           | .NET Core 3.1 | The [Waher.Security.PKCS.Test](Security/Waher.Security.PKCS.Test) project contains unit tests for the  [Waher.Security.PKCS](Security/Waher.Security.PKCS) project.                                         |
| **Waher.Security.SHA3.Test**           | .NET Core 3.1 | The [Waher.Security.SHA3.Test](Security/Waher.Security.SHA3.Test) project contains unit tests for the  [Waher.Security.SHA3](Security/Waher.Security.SHA3) project. |
| **Waher.Security.SPF.Test**            | .NET Core 3.1 | The [Waher.Security.SPF.Test](Security/Waher.Security.SPF.Test) project contains unit tests for the  [Waher.Security.SPF](Security/Waher.Security.SPF) project. |

Services
----------------------

The [Services](Services) folder contains standalone service applications.

| Project                    | Type       | Link                                                          | Project description |
|----------------------------|------------|---------------------------------------------------------------|---------------------|
| **Waher.Service.GPIO**     | UWP        |                                                               | The [Waher.Service.GPIO](Services/Waher.Service.GPIO) project defines a Universal Windows Platform application that can be installed on Windows 10 IoT devices. It will publish available GPIO inputs/outputs over XMPP sensor, control and chat interfaces. It will also publish Digital and Analog Arduino interfaces, if an Arduino using the Firmata protocol is connected to an USB port of the device. The application can be used to elaborate with GPIO peripherals using a simple chat client. |
| **Waher.Service.PcSensor** | .NET 4.6.2 | [Executable](Executables/Waher.Service.PcSensor.zip?raw=true) | The [Waher.Service.PcSensor](Services/Waher.Service.PcSensor) project defines an application that converts your PC into an IoT sensor, by publishing performace counters as sensor values. [Full Screen Shot 1.](Images/Waher.Service.PcSensor.1.png) [Full Screen Shot 2.](Images/Waher.Service.PcSensor.2.png) [Full Screen Shot 3.](Images/Waher.Service.PcSensor.3.png) |

Themes
----------------------

The [Themes](Themes) folder contains libraries that contain content files for different visual themes.

| Project                       | Type         | Link                                                               | Project description |
|-------------------------------|--------------|--------------------------------------------------------------------|---------------------|
| **Waher.Theme.CactusRose**    | .NET Std 1.0 | [NuGet](https://www.nuget.org/packages/Waher.Theme.CactusRose/)    | The [Waher.Theme.CactusRose](Themes/Waher.Theme.CactusRose) project contains content files for the Cactus Rose theme.          |
| **Waher.Theme.GothicPeacock** | .NET Std 1.0 | [NuGet](https://www.nuget.org/packages/Waher.Theme.GothicPeacock/) | The [Waher.Theme.GothicPeacock](Themes/Waher.Theme.GothicPeacock) project contains content files for the Gothic Peacock theme. |
| **Waher.Theme.Retro64**       | .NET Std 1.0 | [NuGet](https://www.nuget.org/packages/Waher.Theme.Retro64/)       | The [Waher.Theme.Retro64](Themes/Waher.Theme.Retro64) project contains content files for the Retro-64 theme.                   |
| **Waher.Theme.SpaceGravel**   | .NET Std 1.0 | [NuGet](https://www.nuget.org/packages/Waher.Theme.SpaceGravel/)   | The [Waher.Theme.SpaceGravel](Themes/Waher.Theme.SpaceGravel) project contains content files for the Space Gravel theme.       |
| **Waher.Theme.WinterDawn**    | .NET Std 1.0 | [NuGet](https://www.nuget.org/packages/Waher.Theme.WinterDawn/)    | The [Waher.Theme.WinterDawn](Themes/Waher.Theme.WinterDawn) project contains content files for the Winter Dawn theme.          |

Things
----------------------

The [Things](Things) folder contains libraries that define a hardware and data abstraction layer for interacting with things. This includes describing 
sensor data, control parameters, attributes, displayable parameters, commands, queries and data sources. It also includes embedding things dynamically,
to form more complex devices, such as concentrators or bridges.

| Project                   | Type         | Link                                                           | Project description |
|---------------------------|--------------|----------------------------------------------------------------|---------------------|
| **Waher.Things**          | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Things/)          | The [Waher.Things](Things/Waher.Things) project is a class library that provides basic abstraction of things, errors, sensor data and control operations. |
| **Waher.Things.Arduino**  | UWP          | [NuGet](https://www.nuget.org/packages/Waher.Things.Arduino/)  | The [Waher.Things.Arduino](Things/Waher.Things.Arduino) project is a class library that publishes nodes for interaction with Arduinos and connected modules via Firmata. |
| **Waher.Things.Gpio**     | UWP          | [NuGet](https://www.nuget.org/packages/Waher.Things.Gpio/)     | The [Waher.Things.Gpio](Things/Waher.Things.Gpio) project is a class library that publishes nodes for interaction with onboard General Purpose Input/Output (GPIO) modules. |
| **Waher.Things.Ip**       | .NET Std 2.0 | [NuGet](https://www.nuget.org/packages/Waher.Things.Ip/)       | The [Waher.Things.Ip](Things/Waher.Things.Ip) project is a class library that publishes nodes representing nodes on an IP network. |
| **Waher.Things.Metering** | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Things.Metering/) | The [Waher.Things.Metering](Things/Waher.Things.Metering) project is a class library that defines a basic metering infrastructure. |
| **Waher.Things.Mqtt**     | .NET Std 2.0 | [NuGet](https://www.nuget.org/packages/Waher.Things.Mqtt/)     | The [Waher.Things.Mqtt](Things/Waher.Things.Mqtt) project is a class library that publishes nodes representing devices connected to MQTT brokers. |
| **Waher.Things.Snmp**     | .NET Std 1.3 | [NuGet](https://www.nuget.org/packages/Waher.Things.Snmp/)     | The [Waher.Things.Snmp](Things/Waher.Things.Snmp) project is a class library that publishes nodes representing SNMP devices on the local area network. |

Utilities
----------------------

The [Utilities](Utilities) folder contains applications that help the developer or administrator with different tasks.

| Project                            | Type          | Link  | Project description |
|------------------------------------|---------------|-------|---------------------|
| **Waher.Utility.Acme**             | .NET 4.6.2    |       | The [Waher.Utility.Acme](Utilities/Waher.Utility.Acme) is a command-line tool that helps you create certificates using the Automatic Certificate Management Environment (ACME) v2 protocol. |
| **Waher.Utility.AnalyzeClock**     | .NET Core 3.1 |       | The [Waher.Utility.AnalyzeClock](Utilities/Waher.Utility.AnalyzeClock) is a command-line tool that helps you analyze the difference in clocks between machines compatible with the [IEEE XMPP IoT extensions](https://gitlab.com/IEEE-SA/XMPPI/IoT). |
| **Waher.Utility.AnalyzeDB**        | .NET Core 3.1 |       | The [Waher.Utility.AnalyzeDB](Utilities/Waher.Utility.AnalyzeDB) is a command-line tool that helps you analyze an object database created by the [Waher.Persistence.Files](Persistence/Waher.Persistence.Files) or [Waher.Persistence.FilesLW](Persistence/Waher.Persistence.FilesLW) libraries, such as the IoT Gateway database. |
| **Waher.Utility.Asn1ToCSharp**     | .NET Core 3.1 |       | The [Waher.Utility.Asn1ToCSharp](Utilities/Waher.Utility.Asn1ToCSharp) is a command-line tool that creates C# files from definitions made in ASN.1 files. |
| **Waher.Utility.Csp**              | .NET Core 3.1 |       | The [Waher.Utility.Csp](Utilities/Waher.Utility.Csp) is a command-line tool that helps you perform operations on keys managed by the system Cryptographic Service Provider CSP. |
| **Waher.Utility.DeleteDB**         | .NET Core 3.1 |       | The [Waher.Utility.DeleteDB](Utilities/Waher.Utility.DeleteDB) is a command-line tool that helps you delete an object database created by the Waher.Persistence.Files or Waher.Persistence.FilesLW libraries, such as the IoT Gateway database, including any cryptographic keys stored in the CSP. |
| **Waher.Utility.ExStat**           | .NET Core 3.1 |       | The [Waher.Utility.ExStat](Utilities/Waher.Utility.ExStat) is a command-line tool that helps you extract statistical information about exceptions occurring in an IoT Gateway, with logging of exceptions activated. |
| **Waher.Utility.Extract**          | .NET Core 3.1 |       | The [Waher.Utility.Extract](Utilities/Waher.Utility.Extract) is a command-line tool that helps you extract information from a backup file generated by the IoT Gateway. |
| **Waher.Utility.GetEmojiCatalog**  | .NET Core 3.1 |       | The [Waher.Utility.GetEmojiCatalog](Utilities/Waher.Utility.GetEmojiCatalog) project downloads an [emoji catalog](http://unicodey.com/emoji-data/table.htm) and extracts the information and generates code for handling emojis. |
| **Waher.Utility.Install**          | .NET Core 3.1 |       | The [Waher.Utility.Install](Utilities/Waher.Utility.Install) is a command-line tool that helps you install pluggable modules into the IoT Gateway. |
| **Waher.Utility.RegEx**            | .NET Core 3.1 |       | The [Waher.Utility.RegEx](Utilities/Waher.Utility.RegEx) is a command-line tool that helps you find content in files using regular expressions, and optionally either export the findings or replace them with something else. |
| **Waher.Utility.RunScript**        | .NET Core 3.1 |       | The [Waher.Utility.RunScript](Utilities/Waher.Utility.RunScript) is a command-line tool that allows you to execute script. |
| **Waher.Utility.Sign**             | .NET Core 3.1 |       | The [Waher.Utility.Sign](Utilities/Waher.Utility.Sign) is a command-line tool that helps you sign files using asymmetric keys. |
| **Waher.Utility.Transform**        | .NET Core 3.1 |       | The [Waher.Utility.Transform](Utilities/Waher.Utility.Transform) is a command-line tool that transforms an XML file utilizing an XSL Transform (XSLT). |

Web Services
----------------------

The [WebServices](WebServices) folder contains modules that add web service capabilities to projects they are used in.

| Project                     | Type         | Link  | Project description |
|-----------------------------|--------------|-------|---------------------|
| **Waher.WebService.Script** | .NET Std 2.0 |       | The [Waher.WebService.Script](WebServices/Waher.WebService.Script) project provides a web service that can be used to execute script on the server, from the client. |
