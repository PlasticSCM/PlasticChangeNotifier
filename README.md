# PlasticChangeNotifier
A Windows utility to watch Plastic SCM repositories and notify changes on the desktop.

# How to run it
Download the plasticnotifier.exe binary from 'releases' (just a single binary thanks to Fody/Costura) or build your own code (remember you can directly pull from Plastic SCM from a GitHub repo).

Then run something as follows to start monitoring your repo:

`plasticnotifier.exe yourepository@yourserver:8087`

You can also use the extra params to specify a 'since' date (useful to see it in action the first time if you can't wait).

`plasticnotifier.exe --cm bcm codice@diana.codicefactory.com:9095 --since "8/18/2016 0:00:00"`
`Monitoring repo codice@diana.codicefactory.com:9095`
`Type 'exit' to quit`
`cm set to bcm`
`bcm find changesets where date >= '8/18/2016 00:00:00' on repository 'codice@diana.codicefactory.com:9095' --nototal --format="[LINE]{changesetid}|||{owner}|||{branch}|||{comment}"`
`CmShell is bcm shell --logo`
`[LINE]6982137|||jesusmg|||/main/SCM18363/SCM18560|||fix javacm`
`[LINE]6982138|||danipen|||/main/SCM18378/scm18539-vio/scm18564|||Preserve diagram position after the incremental loading`

`The toast has timed out`
`The user dismissed the toast`
`bcm find changesets where date >= '8/18/2016 12:06:49' on repository 'codice@diana.codicefactory.com:9095' --nototal --format="[LINE]{changesetid}|||{owner}|||{branch}|||{comment}"`
`bcm find changesets where date >= '8/18/2016 12:07:19' on repository 'codice@diana.codicefactory.com:9095' --nototal --format="[LINE]{changesetid}|||{owner}|||{branch}|||{comment}"`

# Future work
Feel free to pull the code and improve it! There are some pending things:
* Multi-repo watching support.
* Avoid duplicate notifications (just keep a list of changesets already notified).
* Monitor replicas (do a cm find replicationlog to find new replicas, then find the changesets inside each and notify them, even if they were older).
