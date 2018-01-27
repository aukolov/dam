export filename="dam.tar.gz"
echo Removing existing archive...
rm -f $filename
cd src/Dam.Web/ 
echo Creating new archive...
tar -zcvf ../../$filename publish/ 
cd ../..

echo Uploading...
echo "Enter host: "
read host
echo "Enter user: "
read user
scp $filename $user@$host:/var/aspnetcore/deploy/
