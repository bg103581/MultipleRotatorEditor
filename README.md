# MultipleRotatorEditor

Assumptions :
    - I can't modify any of the files already present in the initial project (scripts, prefabs, scene, etc).
    - To select any rotator present in the hierarchy, the user has to either drag and drop one in the "Rotators to edit" field or manually set the number of rotators to edit and then choosing one in the objects menu.
    - The user can put the same rotator several times and the editor will display the rotator as many times.

For me it seems like all the requirements are respected. I'm rather proud of my work knowing that it was the first time I had to custom the editor, however, I had two main issues :
    - When drag and dropping a rotator from the hierarchy into the "Rotators to edit" field, I have this error in the console : "ArgumentException: Getting control 0's position in a group with only 0 controls when doing dragPerform Aborting". I know OnGUI is called for many different events, like dragPerform, and I think I have this error because of the way I'm handling the selection of the rotators. However, I couldn't figure out how not to have this error.
    - When hitting the button "Play", all my displayed selected rotators disappear in the "Selected Rotators" frame. I found out that when I was hitting Play, all my rotators in the "rotatorsToEdit" array become null. I assume it's also because of the way I'm handling the rotators to edit.