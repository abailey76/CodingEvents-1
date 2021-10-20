# CodingEvents

1. Purpose of Application
The purpose of this application is to provide users with information on various coding events happening in a variety of locations across the country.
Users are provided with the name and description of the event, along with a contact email so that they can get in touch with event organizers with any questions.
Events are tagged with keywords for easy searching. Events are also grouped by category.

2. Current State of Application
The app currently allows users to add events, delete events, and edit events. It allows users to add tags and categories, as well as assign tags/categories to various events.
The app contains functionality to list all events, or to list events by category or by tag. Users can click on a given event to view a detail page about that event.

3. Future Improvements to Application
The plan is to add a Person class to the Coding Events application. This class will contain the following fields:

  public string Name
	public string Email
	public int Id
  
The person class will also contain an empty constructor as well as constructor with the name and email passed in as parameters.
An EventsPeople class will also be added to relate the Event and Person classes in a many-to-many relationship.
