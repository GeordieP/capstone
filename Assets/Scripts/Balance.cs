﻿public static class Balance {
	// All types of resources available
	public enum ResourceTypes { Wood, Food, Oil};

	// Amount of resources the respective node offers
	public const int WoodResourceCount = 25;						// Wood from a forest
	public const int FoodResourceCount = 50;						// Food from a farm
	public const int OilResourceCount = 25;							// Oil from an oil rig

	// Amount of resources it will cost to build a new city
	// This amount of each will be taken from the global resource pool each time a city spawns
	public const int CityWoodCost = 500;
	public const int CityFoodCost = 0;
	public const int CityOilCost = 0;

	// Amount of each resource a city will spawn with
	public const int CityWoodStartCount = 250;
	public const int CityFoodStartCount = 0;
	public const int CityOilStartCount = 0;

	// Task times (seconds)
	public const float WoodTaskTime = 2.0f;
	public const float OilTaskTime = 10.0f;
	public const float FoodTaskTime = 7.0f;

	public const float WorldStartHealth = 100.0f;
	public const float WorldStartChInfluence = 0.05f;
	public const float WorldStartCoInfluence = 0.025f;
	public const float WorldStartCh4 = 5.0f;
	public const float WorldStartCo2 = 5.0f;

	// Amount that the CO2 or CH4 level changes by when a forest spawns
	public const float ForestCo2Reduction = 1.0f;
	public const float ForestCh4Reduction = 1.0f;

	// Amount that the CO2 or CH4 level changes by when a city spawns
	public const float CitySpawnCo2Emission = 3.0f;
	public const float CitySpawnCh4Emission = 1.0f;

	public const float CityCo2Emission = 0.05f;
	public const float CityCh4Emission = 0.1f;


}

/*

TO ADD
 * "Full" resource amounts for when nodes are new/full of their resource
	* for example, if a full forest node has 200 wood, and a unit gathers 50 each time, only 4 units will be able to use it before it's depleted.
 * City "happy" values
	* The value for a certain resource type at which point the city has what it needs for now.
		* Going past this will be considered a surplus, and a surplus of enough stuff will trigger a new city to be built
*/