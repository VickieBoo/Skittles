### SkittlesPower 
##### make life colorful :heart:

<br>

> What is SkittlesPower?
>> SkittlesPower is a library I made in my freetime to get colors from images

> How does it work?
>> Methods and their explanations are listed below:

<details>
<summary><b>Average</b> (click to expand)</summary>
    1. Get all colors from the image<br>
    2. Create 3 separate variables called R, G, and B<br>
    3. Assign the sum of color value corresponding to variable name (R = all R values)<br>
    4. Divide each sum by the amount of total colors<br>
    5. Create a new color with the results<br>
    Code Example: 

    var R = colors.Select(color => (int)color.R).Sum() / colors.Count;
    var G = colors.Select(color => (int)color.G).Sum() / colors.Count;
    var B = colors.Select(color => (int)color.B).Sum() / colors.Count;
    return Color.FromArgb(R, G, B);

</details>

<details>
<summary><b>Common</b> (click to expand)</summary>
	1. Get all colors from the image<br>
	2. Find the most common color<br>
	3. return color<br>
	Code Example:
    
    var commons = colors.GroupBy(color => color).OrderByDescending(group => group.Count()).SelectMany(color => color).ToList();
	/* 
      you can do something with the common colors or return the first (most common) color
      I recommend adding Distinct() if you are going to do something with the colors
      it'll get rid of duplicate colors
    */
    return commons[0];
</details>

<details>
<summary><b>Smart</b> (click to expand)</summary>
	1. Get all colors from the image<br>
	2. Get common colors<br>
	3. Get rid of duplicate colors<br>
	4. Group all similar colors<br>
	5. Get the most common groups<br>
	6. return top 3 common colors<br>
	Code Example:
    
    for example visit "SmartAsync.cs" in the "Skittles" folder
</details>
