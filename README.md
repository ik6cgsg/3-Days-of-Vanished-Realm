# 3-Days-of-Vanished-Realm
Interactive VR game for mobile phones (Android/iOS). Full immersion in the world of quest adventures, for the age of 6+. Everyone will have to strain their brains.

Interactive VR game for mobile phones, Android and iOS systems. Full immersion in the world of quest adventures, for the age of 6+. Everyone will have to strain their brains.

The game is first-person, you will:

1) Travel around the map by teleportation (without sharp jumps, so as not to turn the head).

2) Interact with map's objects, using them to solve puzzles.

3) Get a new level with different complexity.

Difficulty will grow with geometric speed. As the result, dive into the world of mysteries with pleasant music in the background.

# The target audience
People, who want to try a full-fledged VR experience without having to buy expensive headsets.

# System requirements
- Google Cardboard v1.0 or similar viewer with magnet button
- Android - 4.4 "KitKat" or higher
- iOS - iPhone 5 or higher with iOS 8.0 or higher

If you are a developer:
- Recommended Unity version: LTS release 2017.4 or higher
- Minimum version for smartphone: 5.6

# Restrictions
Our game is for almost all ages: 3+.
It is recommended to take frequent breaks or not play at all if you experience nausea, discomfort, eye strain, disorientation or you are prone to seizures.

# Getting Started

#### Prepare enviroment
1) [Unity sdk (minimum version: 5.6) ](https://store.unity.com).

2) Development environment under C#:
    - [XCode](https://developer.apple.com/xcode/) for Mac Os 
    - [Visual Studio](https://visualstudio.microsoft.com/ru/?rr=https%3A%2F%2Fwww.google.com%2F) for Windows

#### Development build 
1) Clone repository:

`$ git clone  https://github.com/ik6cgsg/3-Days-of-Vanished-Realm`

2) Open  projects  folder ***3-Days-of-Vanished-Realm*** with Unity.

3) Set compilations preferences:
    - for [iOS instruction](https://developers.google.com/vr/develop/unity/get-started-ios) 
    - for Windows you use automatic setting

4) Build the project, and you will get it for developer mode.

5) In your development environment open file ***.workspace*** or ***.workenvironment***.

6) Connect mobile phone and run project.

# Users installation guide
1) Download: [Unity sdk (minimum version: 5.6) ](https://store.unity.com)

2) Clone repository:

`$ git clone  https://github.com/ik6cgsg/3-Days-of-Vanished-Realm`

3) Open  projects  folder ***3-Days-of-Vanished-Realm*** in Unity.

4) Connect mobile phone, click on "Build" and "Run". 

*(For iOS phone you need Mac or you have to pay for the official license for Windows)*

**Unity will automatically transfer game to phone. Have fun!**

# Analogues
Currently, there are not so many analogues due to underdevelopment of the VR applications market.
But still there are a few worthy games, which ideas prompt us to implement our own.
Here is the list of such projects:

1) ["VR Прятки с Томой"](https://play.google.com/store/apps/details?id=com.garpix.tomavr)
Beautiful app with detailed textures, BUT:
	  * We don't have any "timing" actions
	  * We implement more interesting level design
  	* We have much more comfortable UI, movement and interaction
	
2) ["Cardboard Design Lab"](https://play.google.com/store/apps/details?id=com.google.vr.cardboard.apps.designlab)
Professional vr demo and development tutorial in one thing, BUT:
  	* It's only a demo, not a full game (can't stand our deep worked game)
  	
3) ["Virtual Virtual Reality"](https://play.google.com/store/apps/details?id=com.TenderClaws.VVR)
2017 Google Play Award Winner for “Best VR Experience”,
Unity Awards finalist "Best VR Game", and so on, BUT:
	  * Not a cheap one (our project is open source and absolutely free!)
	  * Requires complex and expensive toolbox (we need only Cardboard) 
Other apps on current market are either poor quality or even dangerous 

# Git workflow
* Main branch for demonstrations - *master*
* Main branch for coding - *develop*
* For feature development branch from *develop*, using this template: <task_number>_<brief_description>
* Merge feature branch only with *develop* branch
* At the end of sprint *develop* merges with *master*
* Do not delete branch after merge!

### Example of branches
```
229_add_shadow_and_lighting
1718_update_html
```
## Commit
* Subject line is **short** summary
* Do not end the subject line with a period
* Do not end the body lines with a period
* Start subject line with issue number after *#*
* Capitalize the subject and the body lines 
* Use the imperative mood in the subject line
* Use the body to explain what and why you have done something
* List with bullet points (use "-" with one space afterwards)
* Use imperative mood

### Example
Subject line:
```
#333 Fix bugs
```
Body:
```
- Remove "+" operator bug
- Remove UI button missclick bug
```

# Code Style
Detailed description is in [CodingStandart.md](https://github.com/ik6cgsg/3-Days-of-Vanished-Realm/blob/master/CodingStandart.md)

# Development team
1) [Denisov Pavel](https://github.com/Ppasha9) - **software engineer**
2) [Vasiliev Peter](https://github.com/pv6) - **techlead**
3) [Kononov Pavel](https://github.com/decentNick) - **software engineer**
4) [Lavrichenko Olga](https://github.com/OLavrik) - **software engineer**
5) [Kozlov Ilya](https://github.com/ik6cgsg) - **teamlead**
