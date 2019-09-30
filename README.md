# CertReqClient
## Client to request SSL certificates with GUI ##

**What is a SSL-Certificate?**
old: SSL = Secure Sockets Layer // new: TLS = Transport Layer Security (TLS is the improvement)

* Ensures that the customer data sent to the server is encrypted (encrypting the communication of data that is transported to a web server through the web browser of a computer). In fact, one can imagine the certificates as "transport levels", over which the data exchange takes place.



**Why do we need an SSL encryption?**
* SSL is used to encrypt the exchange of data between the web browser and the web server, thus protecting it against third-party access. The use of SSL is especially recommended for online shops, forums and websites that have a login area. In order to set up SSL on a web page, and thus to protect all sensitive data of visitors, you need an SSL certificate.
An SSL protected website is professional and gives visitors a sense of security.
(At least since May 2018, all types of personal data in the EU may only be transmitted 	in encrypted form).



**What is a CA/CSR?**
CA: Certificate Authority // CSR: Certificate Signing Request

* CA:
A Certificate Authority (CA) is a trusted organization that publishes electronic documents that can be used to verify identities on the Internet. These electronic documents, which are also called digital certificates, are an essential part of secure communication on the Internet and the Public Key Infrastructure (PKI). A certificate typically includes the owner's public key, the certificate expiration date, the owner's name, and other information about it. Operating systems and browsers already have an internal list of trusted root certificates to validate other certificates issued and signed by a certification authority.

Although any organization that wants to issue digital certificates can do so, most e-commerce providers rely on commercial certification authorities. The duration that a CA is already active 	also plays a role. The longer this is the case, the more browsers and devices usually trust her.	Certificates should be backwards compatible with older browsers and operating systems, this is called Ubiquity.

Protocols based on digital certificate validation are particularly vulnerable to man-in-the-	middle attacks. Examples are VPN and SSL / TLS. Trust in certification bodies has also been 	repeatedly shaken in the past by the misuse of manipulated certificates. For example, 	hackers have been able to penetrate the networks of certification authorities, such as 	DigiNotar and Comodo, and create fake certificates on behalf of companies such as Twitter and Microsoft. In response, DigiCert became the first CA to introduce a new concept called 	Certificate Transparency. This Google-initiated initiative is designed to make it faster to spot 	fake certificates and hacked or malicious certification authorities.



* CSR:
A Certificate Signing Request (CSR) is a disjointed string containing the data selected and entered by the applicant, such as the Internet address to be protected, the certificate holder and his address, and the public key, which is subsequently used by the public the certificate is to be confirmed, connects. With the certification requirement and for the technology primarily the public key, at the same time also the private key is generated, which remains with the applicant, should not be published and since it is later inseparably linked with the certificate, and should also be secured in a secure place.
The certification requirement is digitally confirmed by the certification body after the 	applicant has successfully been checked by the certification body, which often has to 	be 	supplemented. It is also called to digitally sign or just certify the certification 	requirement.
The certificate, which results from this, is again a confused character string, which the 	applicant with the remaining private key on the server imports or integrates 	(depending 	on the server software) and connects with a designated IP. Often the 	server software then 	requires a reboot to put the certificate into operation



**What is a private key/public key pair?**
* A private key is one half of the public/private key pair used in digital certificates. The private key is created before or during the time in which the Certificate Signing Request (CSR) is created. A CSR is a public key that is generated on a server or device according to the server software instructions. The CSR is required during the SSL certificate enrollment process because it validates the specific information about the web server and the organization. The CSR is submitted to a Certificate Authority (CA) which uses it to create a public key to match the private key without compromising the key itself.

The CA never has access to the private key. The private key remains on the server and is never shared. The public key is incorporated into the SSL certificate and shared with clients, which could be a browser, mobile device, or another server. Although the makeup of an SSL certificate consists of a private and public key, the SSL certificate itself is sometimes referred to as "the public key."  The SSL certificate is also referred to as the "end entity" certificate since it sits at the bottom of the certificate chain and is not used for signing/issuing other certificates.

Note: Do not confuse the server's private key with the session key. This is a symmetric key which is created by the browser when it connects to a server. Session keys are typically 128 or 256-bit. The size used depends on the encryption capability of the client and server. The symmetric key is used to encrypt and decrypt information sent back and forth during the SSL session

The 2048-bit SSL certificate and private key (server) is called an asymmetrical key pair. This means that one key is used to encrypt data (the public key/SSL certificate) and the other is used to decrypt data (the private key stored on the server).


## How to get a certificate by using the application? ##

* Actually there are two ways of creating the certificate. The fast way, the one that creates the certificate fully automatic by just one click and the other way that installs the certificate step by step. It's up to you which way you choose. At the end, both will be certifying your webpage.


  **Fully automatic installation**

* First of all, please fill out all necessary fields before clicking the "fully automatic installation" button. Nevertheless the application informs you about the mandatory fields even about the chars that are not accepted by the system. Clicking the "fully automatic installation" Button will now immediately do all necessary steps to install the certificate on your website in just a few seconds. It creates the request file, the private key and the certificate file.

* You should now be able to see the certificate in your Internet Information Services(IIS).

* Installation completed!


  **Installation step by step**
  
* First of all, please fill out all necessary fields before clicking the "generate csr" button. Nevertheless the application informs you about the mandatory fields even about the chars that are not accepted by the system. Clicking the "generate csr" Button now will give you a summary about the data you entered and the possibility to go back if some input is wrong.
* Clicking the "create certificate file" Button will ask you where to safe the .inf file. At the same time a .req file (Request File) will be created too.
* After successfully creating the request file the application will be asking you whether you want to create the private key or not. Click "yes" for continuing the process or "no" to exit the program.
* To continue the process you have now to choose the desired request file (.req file) so that the private key (.pfx file) and the certificate (.cer file) can be created.
* Again, you can now choose to stop here e.g. if the certificate should not be finalized yet or go further on which is to finalize the installation of the certificate that lists this new certificate in your Internet Information Services.

* Installation completed!

## Where to find the releases? ##

* Click the following link to get the releases: https://github.com/kuk-is/CertReqClient/releases
