public interface ISatellite
{
	/* orbit speed */
	float OrbitSpeed { get; set; }
	
	/* larger orbit radius */
	float MaxOrbitRadius { get; }

	/* smaller orbit radius */
	float MinOrbitRadius { get; }
	
	void TraversePath();
	
}
