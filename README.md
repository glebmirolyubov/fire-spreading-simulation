# Fire Spreading Simulation - Unity 2019.1.9f
This simulation uses a simple propagation technique to spread the fire across the terrain with the wind.  

*completed by Gleb Mirolyubov, 2019*    

## HOW IT WORKS
The Unity terrain is populated with plants that can be in 3 different states: **base**, **burning**, and **burnt**. The plant can only be in one state at a time due to the use of the State pattern, which uses enums. Once the plant starts burning, it will go to burnt state in 3 seconds. The plant can't return back to base unburnt state.

<img src="https://github.com/gleebus/fire-spreading-simulation/blob/master/Fire%20Spreading%20Simulation/Assets/Pictures/Screenshot_3.png" width="256">  <img src="https://github.com/gleebus/fire-spreading-simulation/blob/master/Fire%20Spreading%20Simulation/Assets/Pictures/Screenshot_4.png" width="256">  <img src="https://github.com/gleebus/fire-spreading-simulation/blob/master/Fire%20Spreading%20Simulation/Assets/Pictures/Screenshot_5.png" width="256">  
*the same plant in three different states: base, burning, and burnt*  

Simulation Controller script handles the behaviour of on-screen buttons, such as: Generate, Toggle, Fire, Play Simulation. The number of generated plants is predetermined and can be changed in the Unity Editor. By default, I have set the number of generated plants to 100.  

Each plant has a collider component attached to it. With wind, this collider can stretch and rotate, aligning with the wind direction. This will ensure that the wind affects the dire propagation.  

<img src="https://github.com/gleebus/fire-spreading-simulation/blob/master/Fire%20Spreading%20Simulation/Assets/Pictures/Screenshot_1.png" width="400">  <img src="https://github.com/gleebus/fire-spreading-simulation/blob/master/Fire%20Spreading%20Simulation/Assets/Pictures/Screenshot_2.png" width="400">  
*collider size and rotation depends on wind*  

Once the plant is set by fire, either by mouse click or by method, the script attached to this plant will detect any nearby plant colliders and light them up as well. Since the wind greatly increases the size of the "fire area" of the plant, the propagation will be larger.  

## OPTIMISATION  
I have used the pooling technique to create the specified amount of plants in Awake() function, so to not overload the CPU in the middle of the simulation. When the user adds a new plant with a mouse click, a new plant is instantiated and added to the pool. However, when the terrain is cleared of plants, they are not deleted from the pool, but rather deactivated, and then reset to their base state to be used again.  

Additionaly, since every plant has a collider that should isntantly align with wind values, I have avoided using an Update method there to minimise the CPU load. Instead, I have added a listener method to the wind values, which are changed by UI sliders, so that the plant colliders will be processed only when the user changes the wind, and not on every cycle, as would happen in Update.  

## HOW TO RUN  
This project was built with Unity 2019.1.9. All scripts are annotated with comments and I tried to write it keeping the clean code practices in mind.   

To run the simulation, simply press the play button inside the Unity Editor, or build and executable for your target device (intended for desktop platforms).
