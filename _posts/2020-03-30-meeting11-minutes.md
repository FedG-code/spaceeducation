---
layout: post
title: Meeting 11
location: Discord
categories: [minutes]
date: 2020-03-30 14:20:00 +0000
Apologies: None
---
```
Meeting 11 Minutes: 30th March 2020 Discord
Apologies: Ben
```
Aims for this meeting:
Discuss current problems and next steps
Discuss a timeline for code completion and report writing

# Updates:

 - Discuss current problems and next steps
 - Discuss a timeline for code completion and report writing


# Updates:

## Map/ Visuals Issues:

 - The camera needs to be centred on the unit
 - Movement Algorithm:
 - The move button will be replaced with the space bar
 - Currently isn’t reliable in terms of where it will stop/ how far it will travel. This will be fixed next
 - When the player clicks on the map a path is found and shown in the form of a yellow to red gradient line. This currently doesn’t indicate how far you can go in one turn but may be possible to modify it to be. An alternative is to keep it how it is and just tell the player how many turns it will take them to reach the point they have clicked on.

## Question Pop-ups: 

 - How are we going to code for which questions/ scenarios pop up?: Create seperate folders for each tile type and question type for that tile type using the classes. One function will be called which opens the questions folder for that tile then can pass a category into the function
 - Create a class called game manager. Change load question

