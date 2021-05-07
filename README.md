# SpaceLow
<img src=https://github.com/Kfollen93/SpaceLow/blob/main/Images/Main.png alt="Title Screen">
Play the game online in a web browser here: <a href="https://kfollen.itch.io/spacelow">SpaceLow</a> <br>
<i>*The private repository containing all project files and commit messages may be available upon request.</i> <br>
<br>
A third-person, action-adventure, low-poly, space game, where you must fight your way out after crash landing on an unknown planet.  Each level will provide the player with a new planet and different obstacles to overcome.  You will be tasked with exploring and staying alive while searching for the teleporter off of each planet.

## Development
As I continue to improve my programming skills, I have started to understand how important it is to have readable and reusable code.  I spent a lot of time throughout the project reading more about object oriented programming in general, and furthering my knowledge of the four pillars: Encapsulation, Abstraction, Inheritance, and Polymorphism.  Breaking down a class so that it only has one purpose is something I still struggle with, but upon “completing” our initial build of the project, I went back and refactored many of the scripts.  I often find the Player class to be the most difficult; it is so easy to tag on extra things to this class while you are in the middle of development.  Through refactoring, I was able to remove ~200 lines of code into their own respective classes, properly assign private/public variables, and have a greater understanding of where things connect and where other things can (and should) be hidden.

## Purpose of Creating
Each mini-project I make is intended to further my learning of C# and Unity, as well as the process of game development as a whole.  This was the first project that I worked on with a teammate.  As I am a self-taught developer, I felt it was important to see a project through from start to finish with a teammate.  It is one thing to be able to develop alone, but it is entirely different to develop with others and be able to incorporate their ideas and perspectives.  Completing this project helped open my eyes to a lot of new things such as weekly code discussions, check-ins, progress tracking, etc.  I've always kept some notes and often use my whiteboard to break up complex issues, but it was nice to have shared our progress together in a way that was visual for both of us. Here's one overview of our tracking (in addition to having a game design Word document, and many Discord messages): <br>
<br>
<img src=https://github.com/Kfollen93/SpaceLow/blob/main/Images/TaskList.png alt="Task List">

## Additional Information
My teammate made the majority of the art (I dabbled a bit with some unimpressive trees and grass), as I was more focused on programming.  Tools used: <br>

<ul>
  <li>Unity</li>
  <li>Unity's NavMesh</li>
  <li>Blender</li>
  <li>FL Studio</li>
  <li>Audacity</li>
</ul>
We still had to code all of the enemy logic (<i>see</i> <code>NpcAi.cs</code>), but Unity’s NavMesh streamlined the process for baking walkable and unwalkable areas.
