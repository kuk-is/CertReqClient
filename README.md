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



**How to get an SSL certificate?**
* To apply for SSL, you must create a registration request (CSR) and send it to the certification authority (CA). The tool provided by us simplifies the procedure considerably. This is the certificate of installation of the SSL Certificate to install.

  1. Please fill out the fields for "Domain: / Subject Alternative Names: / Organization: /	Department: / City: / State: / Country: /"
  

Attention: It is especially important that you enter the really correct address for "Domain", which should later be certified.

  2. Now click on "Generate" to install the certificate.

  Here we spare you the step, to send the request manually to the internal CA and to 	start the command line as administrator such as to have to call the following 	commands... certreq -submit -attrib "CertificateTemplate: webserver" <filename 	from 2.> <certificate name> .cer <private name Key> .pfx and the subsequent import 	of the certificates with the command certreq -accept <certificate name> .cer no 	longer need you, because the program was designed so that the command input in 	the background is automatic.

  3. You will be asked to enter a file name and the path where the security certificate should be stored. The certificate ends in .cer
