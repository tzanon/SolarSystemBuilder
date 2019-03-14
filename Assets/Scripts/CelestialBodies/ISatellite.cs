public interface ISatellite
{
	/* orbit speed */
	float OrbitSpeed { get; set; }
	
	/* the body this one orbits */
	CelestialBody Primary { get; set; }

	/* larger orbit radius */
	float MaxOrbitRadius { get; }

	/* smaller orbit radius */
	float MinOrbitRadius { get; }

	/* path of the orbit */
	OrbitPath OrbitalPath { get; set; }

	/* circular/ellipsoid region this satellite occupies and travels through */
	OrbitRegion OrbitalRegion { get; }
	
	void TraversePath();
	
}
