# Recipe Box

#### _Webpage to Save and Search Recipes_

#### By _**Shruti Priya && Cassie Musolf_

## Description

This website will let users enter and save movies along with affiliated actors and characters.

## Setup/Installation Requirements

* Requires DNU, DNX, and Mono
* Clone to local machine
* Use command "dnu restore" in command prompt/shell
* Use command "dnx kestrel" to start server
* Navigate to http://localhost:5004 in web browser of choice

## Specifications

**The user can add a recipe**
* Example Input: "Spaghetti"
* Example Output: "Spaghetti"

**The user can add a category for their recipe**
* Example Input: "Italian Food"
* Example Output: "Spaghetti - Italian Food"

**The user can add multiple categories for a recipe**
* Example Input: "Dinner"
* Example Output: "Spaghetti - Dinner - Italian Food"

**The user can update/edit a recipe**
* Example Input: "Spagetti"
* Example Output: "Spaghetti"

**The user can delete a recipe**
* Example Input: *delete clicky*
* Example Output: "Deleted - No such recipe is found"

**The user can update and delete a category**
* Example Input: "Spaghetti - Dinner - Italian Food" *delete dinner category*
* Example Output: "Spaghetti - Italian Food"

**The user can edit a restaurant**
* Example Input: "French Cuisine" "Restaurant du Cheese"
* Example Output: "French Cuisine Restaurants: Restaurant du Fromage"

**The user can rate a recipe**
* Example Input: "5 Stars"
* Example Output: "Awesome Dish!"

**The user can search recipe by a specific ingredient**
* Example Input: "Chicken"
* Example Output: "Butter Chicken, Kadahi Chicken, Tandoori Chicken"

**The user can see a list of recipes in order of highest rating(descending order)**
* Example Input: *5 star, 4star, 3star, 2star, 1star*
* Example Output: "Spaghetti(5star rating),
                  Fried rice (4star) and so on. "

### Icebox

**The user can choose a recipe based on cooking time**
* Example Input: *15 minutes*
* Example Output: "Pasta"

**The user cannot add a duplicate category**
* Example Input: "French Recipe"
* Example Output: "Error: This recipe is already on the list"

**The user cannot add a duplicate recipe**
* Example Input: "Spaghetti"
* Example Output: "Error: This recipe is already on the list"

**The user can click picture of their recipe and post it.

## Support and contact details

Please contact Cassie Musolf at cassiemusolf@gmail.com, or Shruti Priya at shrutipriya1808@gmail.com with any questions, concerns, or suggestions.

## Technologies Used

This web application uses:
* Nancy
* Mono
* DNVM
* C#
* Razor

### License

*This project is licensed under the MIT license.*

Copyright (c) 2017 **_Cassie Musolf && Shruti Priya_**
