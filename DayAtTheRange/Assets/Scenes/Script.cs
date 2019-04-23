using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour {
    /*
     * There are two methods to doing menus. One is with a new scene another is with a game object which has buttons attached to it.
     * UI elements are 2D. Try locking your view to 2D when editing the Canvas object or its elements. In the Tanks tutorial, there is a
     * good description of how this all works. Couple set up things:
     * 1. Set Game view to whatever your display will be for exported format.
     * 2. Screenspace overlay for Huds, World Space is for Actual Text Elements
     * 3. Canvas Scalar, always set the "Scale with Screen Size", you'll want to make your Reference Resolution to the size of your build's aspect/pixel ratio
     * 4. You don't really want anything else different, now you have can place UI elements in a way, which makes more sense
     * Keep in mind that your text has an invisble box around it to define the acceptable bounds of its placement.
     * On to Buttons:
     * Buttons are two objects. The button and the text.
     * Anyway, it you want a pause menu you could do the same thing with a hide menu button and place the menu objects into a panel and then have that mapped to a keypress
     * 
     */
}
