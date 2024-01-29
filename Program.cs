public class CollisionSimulation
{

	// Calculating final velocities of cubes that have a perfectly elastic collision
	public static (double, double) SimulateCollision(Cube cube1, Cube cube2)
	{
		return (
			(cube1.mass - cube2.mass) / 
			(cube1.mass + cube2.mass) * 
			cube1.speed + (2 * cube2.mass) / 
			(cube1.mass + cube2.mass) * 
			cube2.speed,

			(2 * cube1.mass) / 
			(cube1.mass + cube2.mass) * 
			cube1.speed + (cube2.mass - cube1.mass) / 
			(cube1.mass + cube2.mass) * 
			cube2.speed
			);
	}

	public class Cube
	{
		public double mass;
		public double speed;

		public Cube(double _mass, double _speed)
		{
			mass = _mass;
			speed = _speed;
		}
	}
}

class Program
{
	public static int Main()
	{
		Console.Write("Enter how many digits of Pi you want to find -> ");

		string input = Console.ReadLine() ?? ""; // Doing more than 7 may take a while on slower machines
		int power;

		try
		{
			power = Int32.Parse(input);
		} catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
			Console.WriteLine("Input format is not correct -> defaulting to 5");
			power = 5;
		}

		CollisionSimulation.Cube cube1 = new(1 , 0); // Small cube will have mass of 1 and will no be moving

		/* Large cube will have a mass of 100 ^ n where n is an integer and the velocity must be negative because 
		 * this program is setup so that the large cube is on the right and the small cube is on the left and the wall
		 * is on the left
		 */
		CollisionSimulation.Cube cube2 = new(Math.Pow(100, power), -1);


		long collisions = 0; // Amount of coliisions which will equal digits of PI

		/*
		 * Cube2 wil only collide with cube1 if its velocity after collision with wall is less than cube1
		 */
		while (cube2.speed < cube1.speed)
		{
			(cube1.speed, cube2.speed) = CollisionSimulation.SimulateCollision(cube1, cube2); collisions++; // Simulate collision

			/*
			 * There can not be a collision with the wall if cube1 is not going towards the left
			 * It must have a velocity less than 1
			 */
			if (cube1.speed >= 0) break;

			cube1.speed *= -1; collisions++; // Collision with wall. The velocity of cube is multiplied by -1 to go towards the right
		}

		Console.WriteLine($"Pi = {collisions / Math.Pow(10, power)}");

		Console.Write("Press any key to exit program...");
		Console.ReadLine(); // Prevent closing of terminal without seeing output

		return 0;
	}
}