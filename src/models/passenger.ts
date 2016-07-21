public class Passenger {

	private String UID;
	private Point origin;
	private Point destination;
	
	public Passenger(String name) {
		this(name, (Point)null, (Point)null);
	}
	
	public Passenger(String name, Point origin, Point destination) {
		this.setUID(name);
		this.setOrigin(origin);
		this.setDestination(destination);
	}
	
	public Point getOrigin() {
		return origin;
	}

	public void setOrigin(Point origin) {
		this.origin = origin;
	}

	public Point getDestination() {
		return destination;
	}

	public void setDestination(Point destination) {
		this.destination = destination;
	}

	public String getUID() {
		return UID;
	}

	public void setUID(String uID) {
		UID = uID;
	}	
}