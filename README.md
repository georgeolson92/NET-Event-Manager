# Event-Manager

#### An event manager that pulls information on local live music events. Will be integrated with <a href="https://www.bandsintown.com/api/overview">Bandsintown API</a> and other functionality in the future.

### Specifications
| Behavior | Input | Output |
|:---  | :---  | :----  |
|It can create a new venue| `"Arlene Schnitzer Concert Hall"`| `"List of venues: Arlene Schnitzer Concert Hall"`|
|It can create a new event with relationship to venue| `"Glass Animals, Arlene Schnitzer Concert Hall"`| `"List of events: Glass Animals at the Arlene Schnitzer Concert Hall"`|
|It can display list of events at a venue| `"Arlene Schnitzer Concert Hall"` | `"List of events at this venue: Glass Animals"` |


### Setup/Installation Requirements

* Open PowerShell and ensure that C&#35; is installed (<a href="https://www.learnhowtoprogram.com/c/getting-started-with-c/installing-c">View link</a> for information on installing C&#35; in PowerShell)
* Ensure that Git project management is functioning (<a href="https://www.learnhowtoprogram.com/c/getting-started-with-c/git-project-setup-for-windows">View link</a> for information on setting up Git in PowerShell)
* Clone repository using "git clone <repository url>.git"
* Go to repository folder, NETEventManager folder, and open the "NETEventManager.sln" file in Visual Studio 2015 (If not already installed please visit <a href="https://www.visualstudio.com/en-us/downloads/download-visual-studio-vs.aspx">this link</a>).
* In PowerShell, navigate to the src\NETEventManager folder and set up your local database using the command "dotnet ef database update"
* View project files in the Solution Explorer section of Visual Studio
* Run application 


### Known Bugs
No known bugs in current version.

### Support and Contact Details
You can reach me via email: **georgeolson92@gmail.com**

### Technologies Used
C&#35;, ASP.NET, Entity, Razor, Bootstrap, HTML, CSS

#### License
MIT

Copyright (c) 2016 **_(George Olson)_**
