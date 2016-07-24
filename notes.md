# Install profile and city
./node_modules/osrm/lib/binding/osrm-extract -p profiles/car.lua bremen-latest.osm.pbf

# Prepare data for routing
./node_modules/osrm/lib/binding/osrm-contract bremen-latest.osrm