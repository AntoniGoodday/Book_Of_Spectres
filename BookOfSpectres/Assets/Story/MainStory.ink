EXTERNAL GoToScene(x)
VAR controllerConnected = false



Phoebe: Neutral
<expression=Angry>Angry
<expression=Smug>Smug
<expression=Think>Think
<expression=Annoyed>Annoyed
<expression=Surprised>Surprised
<expression=Flustered>Flustered
<expression=Neutral>Time to<expression=Flustered> change expressions<expression=Think> couple times in a row ... <expression=Smug> yea!
*   [Choice 1]
    Yea, it works!
*   [Choice 2]
    Still works!{GoToScene("TestScene")}
*   [The funny]
    <expression=Surprised>Look Gordon, ropes!
    <expression=Smug>We can use them for big pits!
- <expression=Neutral>End of dialogue for now.
->DONE


=== tutorial_1 ===
Phoebe: Hello!
Would you like to see the combat tutorial?

*   [Yes]
    Sweet!<delay=0.2> </delay><expression=Smug>I knew you'd make the right choice.
    <expression=Neutral>To move use the { controllerConnected == true:D-PAD or the LEFT STICK|ARROW KEYS or WSAD}.
*   [No]
    Aww, I was really looking forward to it.{GoToScene("BattleScene_2")}
-
-> END

=== tutorial_2 ===
Phoebe: Excellent!
<expression=Think>Huh, what is that noise?
Axolotl: <expression=Neutral><alignment=Right>Shhhhhhhaaaaaa
Phoebe: <expression=Surprised><alignment=Left>Watch out, a spectre!
<expression=Neutral>Try dodging three of its projectiles in a row!
-> END

=== tutorial_3 ===
Phoebe: <expression=Smug>Alright! Time to go on the offensive!
<expression=Neutral>Press { controllerConnected == true: RIGHT BUMPER or A |SPACE} to shoot a projectile. Do this three times.
-> END

=== tutorial_4 ===
Phoebe: <expression=Smug>Perfect!
<expression=Neutral>Now try to unleash a charged attack.
To do so hold { controllerConnected == true: RIGHT BUMPER or A |SPACE} until the particles around me change colour, then release!
-> END

=== tutorial_5 ===
Phoebe: Great job! Now time for the exciting part - spells!
Press { controllerConnected == true: LEFT BUMPER |Q} to open the spell menu.
-> END

=== tutorial_6 ===
Phoebe: Welcome to the spellbook!
Select spells on the bottom of the screen.
Make sure you have enough mana to cast them, indicated by the bar near the top-right of the book.
Choose atleast three spells to add them to the SPELL QUEUE, then press READY to go back into combat.
-> END

=== tutorial_7 ===
Phoebe: Now it's time to end this!
To unleash spells press { controllerConnected == true: X |E}. This will use the first spell you've selected, then move on to the next one.
Try defeating this enemy to finish the tutorial.
Good luck!
-> END

=== tutorial_8 ===
Phoebe: <expression=Smug>Hell yes, I- <expression=Flustered>, ehm, <expression=Neutral> WE did it!
Let's see if we'll be able to escape this weird place.{GoToScene("BattleScene_2")}
-> END

=== battleScene2Start ===
Phoebe: Watch out, two ghosts!
-> END

=== battleScene2End ===
Phoebe: Alright, with those spirits dealt with, where to go next?
*   [Left Tunnel]
    Alright, let's go Left. {GoToScene("BattleScene_3")}
*   [Right Tunnel]
    Alright, let's go Right {GoToScene("BattleScene_4")}
-
-> END

=== battleScene3Start ===
Phoebe: <expression=Surprised>More ghosts?
<expression=Angry>AAAAARGH, let's deal with them already.
-> END

=== battleScene3End ===
Phoebe: <expression=Smug>Done, didn't even break a sweat.{GoToScene("BattleScene_5")}
-> END

=== battleScene4Start ===
Phoebe: <expression=Surprised>Only two ghosts again? <expression=Think> Seems suspicious...
<expression=Surprised>Watch out! There are holes in this arena!
-> END

=== battleScene4End ===
Phoebe: <expression=Neutral>Hell ye, we make a great team.{GoToScene("BattleScene_5")}
-> END

=== battleScene5Start ===
Phoebe: I think we are nearing the end, I'm seeing a ton of spirits coming our way!
-> END

=== battleScene5End ===
Phoebe: <expression=Smug>We did it, all ghosts defeated!
<expression=Neutral>I see some faint light behind them...
I think this is th-{GoToScene("CreditsScene")}
-> END

=== function GoToScene(x) ===
~ return ""