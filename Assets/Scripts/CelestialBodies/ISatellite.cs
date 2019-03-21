public interface ISatellite
{
	/* orbit speed */
	float OrbitSpeed { get; set; }
	
	float OrbitRadius1 { get; set; }

	float OrbitRadius2 { get; set; }

	/* larger orbit radius */
	float MaxOrbitRadius { get; }

	/* smaller orbit radius */
	float MinOrbitRadius { get; }

	CelestialBody Primary { get; }

	OrbitPath Path { get; }

	OrbitRegion Region { get; }

	void InitSatellite(CelestialBody primary, OrbitPath path, float rad1, float rad2);

	void TraversePath();
	
}
