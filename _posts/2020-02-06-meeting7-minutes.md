---
layout: post
title: Meeting 7
location: GO Jones Room 410
categories: [minutes]
date: 2020-03-06 14:00:00 +0000
Apologies: None
---

```
Meeting 7 Minutes: 6th March 2020 GO Jones Room 410
Apologies: None
```

# Aims for this meeting:

 - Fix map bugs
 - Decide what we want each tile to do
 - Next steps

# Map Bugs:

 - Currently the map moves under the camera why?: Due to the hexagonal coordinate system the map is wrapped to make it appear to be a square rather than a trapezium. Wrapping causes distortion when the camera moves so moves the map instead. This however isn’t a practical solution in a game as would make it very laggy if the map basically regenerates every time the camera moves. 
 - Try to fix it using bool false to prevent wrapping
 - Fed will continue working on the map to improve this


# Function of each tile type:

 - We now have an interface and different classes set up for each tile to make it easier to set each tile’s specific function and passability.

## Tiles:

 - Blackhole = impassable
 - Asteroids = only small ships can get through and collect resources (by mining).This may present problems when it comes to the movement algorithm though
 - Stars = Impassible/cannot land on it, may have different sizes and colours if there is time to represent red dwarfs and other star types
 - Planets = can land here, either populated or unpopulated, can then answer questions which depend on the scenario presented and gain or lose resources
 - Event tiles = can land here, random boost with accompanied scenario
 - Bad Planets = can land here, lose health points / resources as get attacked here
