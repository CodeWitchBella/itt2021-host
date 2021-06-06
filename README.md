# Guest

With the help of VR, the viewer finds themselves in a seemingly deserted forest. What causes the presence of an element in an environment where it does not belong?

![First person view from the experience](preview.png)

[Project website](https://projects.iim.cz/itt2/2021/red/index.php) (in czech, contains roughly the same info as this file)

## Longer description

Before starting the viewer downloads a file which they open at a specific time. This will take them to the waiting room from where they will be admitted to the performance. Once the experience is launched, the viewer appears alone in a virtual world they can discover. The world invites the viewer to explore and shows them what it offers. However, the presence of the viewer causes it to change. The experience takes place for several spectators at the same time. They will meet each other during the performance and will also have the opportunity to communicate.

## Necessary equipment for the viewer

- Computer with Windows, MacOS or Linux
- Alternatively to connect using VR you need SteamVR and Windows. Only tested with HTC Vive

## Startup instructions

### Windows

- Download the app from [releases tab](https://github.com/CodeWitchBella/itt2021-host/releases) (ends in `-windows.zip`)
- Unzip the zip file to any folder (right-click, Extract all ..., Extract)
- Double-click the ITT_Host.exe program from the extracted folder
- On the next screen, select desktop or VR mode by pressing D for Desktop or V for VR.
- In desktop mode, the application is controlled using the mouse and WASD keys.

### MacOS

- Download the app from [releases tab](https://github.com/CodeWitchBella/itt2021-host/releases) (ends in `-macos.dmg`)
- Double-click the downloaded file
- Drag Host.app to the Applications
- Run Host.app
- The application is controlled using the mouse and WASD keys

### Linux

- Download the app from [releases tab](https://github.com/CodeWitchBella/itt2021-host/releases) (ends in `-linux.tar.gz` or `-linux.tar.xz`)
- Extract the archive
- Run Host.x86_64
- The application is controlled using the mouse and WASD keys

## Application architecture

The application is programmed in Unity. It is based mainly on the Universal Render Pipeline and built-in support for VR using OpenXR. It is also modified by several scripts that allow it to run on the desktop. Viewers are connected using peer-to-peer technology.

## Technical description

The virtual world is presented to the viewer by means of VR in [Unity](https://unity.com/). Alternatively, if the viewer does not have a VR headset, they can view the world via desktop mode. The scene and individual models are created in [Rhino](https://www.rhino3d.com/) and imported into Unity where the audience shares the world so that they can meet in the final part of the performance. We used the [NormCore](https://normcore.io/) plugin for communication between viewers.

We also used the [Perception Neuron](https://neuronmocap.com/) motion capture system and Axis Studio to capture the dancer's movements. These are mapped to a character model in Unity. Visual effects were created using shader graph in Unity. The music is applied as one track without any dynamic control.

## Creators

- Directing, dramaturgy: Michaela Dzurovčínová, Viktor Prokop
- Concept: whole team
- Choreography: Michaela Dzurovčínová
- Interpretation: Adéla Kašparová
- Scenography and modeling: Dominika Mrkvová, Lea Baniariová, Sofie Gjuričová
- Technical realization: [Isabella Skořepová](https://isbl.cz/), Viktor Bobůrka
- Sound design: Tomáš Formánek
