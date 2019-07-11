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
